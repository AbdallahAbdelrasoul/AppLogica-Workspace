using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Models;

namespace ERB.Services.ElsaServer.Domain.CustomActivities
{
    public class MyCustomActivity : CodeActivity<string>
    {
        public Input<string> UserName { get; set; } = default!;

        protected override void Execute(ActivityExecutionContext context)
        {
            var name = UserName.Get(context);
            var message = $"Hello, {name}!";
            context.SetResult(message);
        }
    }
}
