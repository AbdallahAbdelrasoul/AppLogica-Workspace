using ERB.Services.Workflows.Application.Workers.SendAnnualLeaveRequest.IO;
using Zeebe.Client.Accelerator.Abstractions;
using Zeebe.Client.Accelerator.Attributes;

namespace ERB.Services.Workflows.Application.Workers
{
    [JobType("email")]
    public class SendAnnualLeaveRequestWorker : IAsyncZeebeWorker
    {
        public async Task HandleJob(ZeebeJob job, CancellationToken cancellationToken)
        {
            var variables = job.getVariables<SendAnnualLeaveRequestInput>();
            Console.WriteLine("I'am : SendAnnualLeaveRequestWorker");

            // You should handle the concurrent execution of sending email
            await Task.Run(() =>
            {
                foreach (var userId in variables.UserIds)
                {
                    Console.WriteLine($"Email is sent to {userId}");
                }
            }, cancellationToken);

        }
    }
}
