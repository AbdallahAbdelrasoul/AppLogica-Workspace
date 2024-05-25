using ERB.Services.Workflows.Application.Workers.CheckAnuuals.IO;
using Zeebe.Client.Accelerator.Abstractions;
using Zeebe.Client.Accelerator.Attributes;

namespace ERB.Services.Workflows.Application.Workers
{
    [JobType("check-annual-days")]
    public class CheckAnnualDaysWorker : IAsyncZeebeWorkerWithResult<CheckAnnualOutput>
    {
        public async Task<CheckAnnualOutput> HandleJob(ZeebeJob job, CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            Console.WriteLine("I'am : CheckAnnualDaysWorker");
            return new CheckAnnualOutput
            {
                AvailDays = 19
            };
        }
    }
}
