using Elsa.Workflows.Management.Contracts;
using Elsa.Workflows.Runtime.Contracts;
using Elsa.Workflows.Runtime.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace ERB.Services.ElsaServer.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class WorkflowsController : ControllerBase
    {
        private readonly IWorkflowRuntime _workflowRuntime;
        private readonly IWorkflowInstanceStore _workflowInstanceStore;
        public WorkflowsController(IWorkflowRuntime workflowRuntime, IWorkflowInstanceStore workflowInstanceStore)
        {
            _workflowRuntime = workflowRuntime;
            _workflowInstanceStore = workflowInstanceStore;
        }

        [HttpGet("[Action]/{definitionId}")]
        public async Task<ActionResult> Start([FromRoute] string definitionId)
        {
            var res = await _workflowRuntime.StartWorkflowAsync(definitionId);
            return Ok(res);
        }

        [HttpGet("[Action]/{instanceId}/{bookmarkName}")]
        public async Task<ActionResult> Resume([FromRoute] string instanceId, string bookmarkName)
        {
            var workflowInstance = await _workflowInstanceStore.FindAsync(
                new Elsa.Workflows.Management.Filters.WorkflowInstanceFilter
                {
                    Id = instanceId
                });

            var result = await _workflowRuntime
                .ResumeWorkflowAsync(instanceId,
                new ResumeWorkflowRuntimeParams
                {
                    BookmarkId = workflowInstance?.WorkflowState.Bookmarks.Single(x => x.Name == bookmarkName).Id
                });

            return Ok(result);

        }
    }
}
