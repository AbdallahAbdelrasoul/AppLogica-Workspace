using ERB.Services.Workflows.Presentation.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zeebe.Client;

namespace ERB.Services.Workflows.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CamundaController : ControllerBase
    {
        private readonly IZeebeClient _zeebeClient;

        public CamundaController(IZeebeClient zeebeClient)
        {
            _zeebeClient = zeebeClient;
        }

        [HttpPost(Name = "AnuualLeaveRequest")]
        public async Task<ActionResult<long>> Run([FromBody] AnuualLeaveRequestInput input)
        {
            var sendAnnualLeaveRequestInput = JsonConvert.SerializeObject(input.SendAnnualLeaveRequestInput);

            Console.WriteLine(typeof(Program).Assembly);

            var processInstanceResponse = await _zeebeClient
                    .NewCreateProcessInstanceCommand()
                    .BpmnProcessId("annual-leave-request")
                    .LatestVersion()
                    .Variables(sendAnnualLeaveRequestInput)
                    .Send();

            return Ok(processInstanceResponse.ProcessInstanceKey);
        }

        [HttpGet("CompleteUserTask/{jobkey}")]
        public async Task<ActionResult> CompleteUserTask([FromRoute] long jobkey)
        {
            return Ok(await _zeebeClient.NewCompleteJobCommand(jobkey).Send());
        }
    }
}
