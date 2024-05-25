using Zeebe.Client.Accelerator.Abstractions;
using Zeebe.Client.Accelerator.Attributes;

namespace ERB.Services.Workflows.Application.Workers
{
    [JobType("io.camunda.zeebe:userTask")]
    [AutoComplete(false)]
    public class WaitApprovalsWorker : IAsyncZeebeWorker
    {
        public async Task HandleJob(ZeebeJob job, CancellationToken cancellationToken)
        {
            var jobkey = job.Key;
            var variables = job.Variables;
            Console.WriteLine("WaitApprovalsWorker variables :" + variables);

            // store job key to be completed using it
            Console.WriteLine("WaitApprovalsWorker jobkey :" + jobkey);
        }
    }
}
