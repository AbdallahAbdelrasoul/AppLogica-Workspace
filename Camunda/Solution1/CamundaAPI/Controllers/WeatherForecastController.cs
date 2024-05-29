using ERB.Services.EInvoice.Domain.Shared.Product;
using ERP.Services.CRM.Application.Shared.Emails;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using System.Net;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;
using Zeebe.Client.Impl.Builder;

namespace CamundaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IEmailsService _emailsService;
        private readonly IProductService _productsService;
        public WeatherForecastController(IEmailsService emailsService, IProductService productsService)
        {
            _emailsService = emailsService;
            _productsService = productsService;
        }

        private static readonly string ClientID = "VjZoKRe__39ZhOHkRZlRSBDSjOeE-7.0";
        private static readonly string ClientSecret = "UBmO2d-Yy48Yhuw_2mOTpVSIMWIQJWgOJk5wUOpvZyMRAEVBW_oNXHnJU0QFEWMt";
        private static readonly string ClusterAddress = "20ec002e-6764-49f4-9480-6be21a0de691.syd-1.zeebe.camunda.io:443";
        private static readonly ILoggerFactory LoggerFactory = new NLogLoggerFactory();
        private static readonly ILogger<Program> Log = LoggerFactory.CreateLogger<Program>();
        private const string LogMessage = "Started instance for" +
                                          " processDefinitionKey='{processDefinitionKey}'," +
                                          " bpmnProcessId='{bpmnProcessId}'," +
                                          " version='{version}'" +
                                          " with processInstanceKey='{processInstanceKey}'";

        private const string VariablesKey = "message_content";
        private const string VariablesValue = "Hello from the C# get started";
        private EventWaitHandle SendEmailWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        private EventWaitHandle CkeckAnuualWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        private EventWaitHandle ApprovalsWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);


        [HttpGet("TestCamunda/{userIds}")]
        public async Task<ActionResult> TestCamunda([FromRoute] string userIds)
        {
            using (var zeebeClient = CreateZeebeClient())
            {
                var topology = await zeebeClient.TopologyRequest().Send();

                Console.WriteLine("Topology: " + topology);

                await zeebeClient.NewDeployCommand().AddResourceFile("check-days_send-email_approvance.bpmn").Send();

                var variables = $"{{\"{VariablesKey}\":\"{VariablesValue}\"" +
                    $", \"UserId\":\"{userIds}\"}}";

                var processInstanceResponse = await zeebeClient
                    .NewCreateProcessInstanceCommand()
                    .BpmnProcessId("annual-leave-request")
                    .LatestVersion()
                    .Variables(variables)
                    .Send();


                //var PublishMessageResponse = await zeebeClient
                //  .NewPublishMessageCommand()
                //  .MessageName("annual-leave-request")
                //  .CorrelationKey("123")                  
                //  .Variables(variables)
                //  .Send();

                Log.LogInformation(LogMessage,
                    processInstanceResponse.ProcessDefinitionKey,
                    processInstanceResponse.BpmnProcessId,
                    processInstanceResponse.Version,
                    processInstanceResponse.ProcessInstanceKey);


                using (CkeckAnuualWaitHandle)
                {
                    var checkAnnualDaysWorker = zeebeClient.NewWorker()
                        .JobType("check-annual-days")
                        .Handler(CheckAnnualDaysJobHandler)
                        .MaxJobsActive(5)
                        .Name("Check Annual Days")
                        //.AutoCompletion()
                        .PollInterval(TimeSpan.FromSeconds(1))
                        .Timeout(TimeSpan.FromMilliseconds(10000))
                        .Open();

                    CkeckAnuualWaitHandle.WaitOne();
                }
                using (SendEmailWaitHandle)
                {
                    var emailWorker = zeebeClient.NewWorker()
                       .JobType("email")
                       .Handler(SendEmailJobHandler)
                       .MaxJobsActive(5)
                       .Name("Send Email Worker")
                       //.FetchVariables(variables)
                       //.AutoCompletion()
                       .PollInterval(TimeSpan.FromSeconds(1))
                       .Timeout(TimeSpan.FromMilliseconds(10000))
                       .Open();

                    SendEmailWaitHandle.WaitOne();
                }
                using (ApprovalsWaitHandle)
                {
                    var emailWorker = zeebeClient.NewWorker()
                       .JobType("io.camunda.zeebe:userTask")
                       .Handler(WaitApprovalsJobHandler)
                       .MaxJobsActive(5)
                       .Name("Wait For Approvals")
                       //.FetchVariables(variables) : set job variables 
                       //.AutoCompletion()
                       .PollInterval(TimeSpan.FromSeconds(1))
                       .Timeout(TimeSpan.FromMilliseconds(10000))
                       .Open();

                    ApprovalsWaitHandle.WaitOne();
                }
            }
            return Ok();
        }


        private async void CheckAnnualDaysJobHandler(IJobClient jobClient, IJob activatedJob)
        {
            var variables = JsonConvert.DeserializeObject<Dictionary<string, string>>(activatedJob.Variables);

            var product = await _productsService.GetProduct("", 123);

            Console.WriteLine($"Avail Annual Days: {product.CurrentBalance}");
            var outVariables = $"{{\"AvailDays\":{product.CurrentBalance}}}";

            await jobClient.NewCompleteJobCommand(activatedJob)
                .Variables(outVariables)
                .Send()
                .ContinueWith(task =>
                {
                    CkeckAnuualWaitHandle.Set();
                });
        }
        private async void SendEmailJobHandler(IJobClient jobClient, IJob activatedJob)
        {
            var variables = JsonConvert.DeserializeObject<Dictionary<string, string>>(activatedJob.Variables);

            var sent = await _emailsService.SendEmail(new Platform.Services.SendGrid.API.SendEmailInput
            {
                FromName = "Camunda8",
                Subject = "Test Camunda8",
                PlainTextContent = "Test Camunda8 Send Email",
                ToEmail = "abdallahabdelrasouludacity@gmail.com",
                ToName = "A.Mohamed"
            });

            Console.WriteLine($"Sending email with message content: {variables[VariablesKey]}");

            await jobClient.NewCompleteJobCommand(activatedJob)
                .Send()
                .ContinueWith(task =>
                {
                    SendEmailWaitHandle.Set();
                });
        }

        private async void WaitApprovalsJobHandler(IJobClient jobClient, IJob activatedJob)
        {
            var variables = JsonConvert.DeserializeObject<Dictionary<string, string>>(activatedJob.Variables);

            var product = await _productsService.GetProduct("", 123);

            Console.WriteLine($"Notification is sent to {variables["UserId"]} with job key : {activatedJob.Key}");

            ApprovalsWaitHandle.Set();
            //var outVariables = $"{{\"approved\":\"True\"}}";

            //await jobClient.NewCompleteJobCommand(activatedJob)
            //    .Variables(outVariables)
            //    .Send();
        }

        [HttpGet("CompleteUserTask/{jobkey}")]
        public async Task<ActionResult> CompleteUserTask([FromRoute] long jobkey)
        {
            using (var zeebeClient = CreateZeebeClient())
            {
                var pse = await zeebeClient.NewCompleteJobCommand(jobkey).Send();
            }
            return Ok();
        }

        private static IZeebeClient CreateZeebeClient()
        {
            return CamundaCloudClientBuilder
                .Builder()
                .UseClientId(ClientID)
                .UseClientSecret(ClientSecret)
                .UseContactPoint(ClusterAddress)
                .UseLoggerFactory(LoggerFactory) // optional
                .Build();
        }
    }
}
