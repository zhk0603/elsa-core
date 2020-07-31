using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elsa.Models;
using Elsa.Persistence;
using Elsa.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elsa.Dashboard.Web.Controllers
{
    [Route("api/work-flow")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowInvoker invoker;
        private readonly IWorkflowDefinitionStore definitionStore;
        private readonly IWorkflowInstanceStore instanceStore;

        public WorkflowController(IWorkflowInvoker invoker,
            IWorkflowDefinitionStore definitionStore,
            IWorkflowInstanceStore instanceStore)
        {
            this.invoker = invoker;
            this.definitionStore = definitionStore;
            this.instanceStore = instanceStore;
        }

        [HttpPost("start")]
        public async Task StartWorkFlow(string definitionId) 
        {
            var definition = await definitionStore.GetByIdAsync(definitionId, VersionOptions.Latest);
            var contexts = await invoker.StartAsync(definition);
        }

        [HttpPost("resume")]
        public async Task ResumeWorkFlow(string wfInstanceId, string currentActivityId)
        {
            var instance = await instanceStore.GetByIdAsync(wfInstanceId);
            var contexts = await invoker.ResumeAsync(instance, Variables.Empty, new[] {currentActivityId});
        }

        [HttpPost("fallback")]
        public async Task FallbackWorkFlow(string wfInstanceId, string blockingActivityId)
        {
            var instance = await instanceStore.GetByIdAsync(wfInstanceId);
            var contexts = await invoker.FallbackAsync(instance, Variables.Empty, new[] { blockingActivityId });
        }
    }
}
