using Elsa.Workflows;

namespace ERB.Services.ElsaServer.Domain.CustomActivities
{
    public class WaitForApproval : Activity
    {
        protected override void Execute(ActivityExecutionContext context)
        {
            // Create a bookmark. The created bookmark will be stored in the workflow state.
            var bookmark = context.CreateBookmark();

            // This activity does not complete until the event occurs.
        }
    }
}
