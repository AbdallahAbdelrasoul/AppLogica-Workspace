using Elsa.Workflows;
using Elsa.Workflows.Models;

namespace ERB.Services.ElsaServer.Domain.CustomActivities
{
    public class WaitForApproval : Activity
    {
        protected override void Execute(ActivityExecutionContext context)
        {
            // Create a bookmark. The created bookmark will be stored in the workflow state.
            CreateBookmarkArgs args = new CreateBookmarkArgs();
            args.BookmarkName = "myBookmark";
            var bookmark = context.CreateBookmark(args);

            // This activity does not complete until the event occurs.
        }
    }
}
