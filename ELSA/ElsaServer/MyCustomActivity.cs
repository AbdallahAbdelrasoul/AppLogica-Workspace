using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Models;

namespace ElsaServer
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

    public class WaitForApproval : Activity
    {
        protected override void Execute(ActivityExecutionContext context)
        {
            // Create a bookmark. The created bookmark will be stored in the workflow state.
            context.CreateBookmark();

            // This activity does not complete until the event occurs.
        }
    }
}
