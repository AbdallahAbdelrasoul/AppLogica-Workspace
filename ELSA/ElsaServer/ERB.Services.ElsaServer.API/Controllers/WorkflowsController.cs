using Microsoft.AspNetCore.Mvc;

namespace ERB.Services.ElsaServer.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WorkflowsController : ControllerBase
    {
        //private readonly IWorkflowRuntime _workflowRuntime;
        //public WorkflowsController(IWorkflowRuntime workflowRuntime)
        //{
        //    _workflowRuntime = workflowRuntime;
        //}

        [HttpGet("aaa")]
        public async Task<ActionResult> StartWorkflow()
        {
            //await _workflowRuntime.StartWorkflowAsync("Workflow 1");
            return Ok();
        }
    }
}
