using System.Threading;
using System.Threading.Tasks;
using Elsa.Services;
using Elsa.Services.Models;

namespace Elsa.Results
{
    public class FallbackResult : ActivityExecutionResult
    {
        public FallbackResult()
        {
        }

        public override async Task ExecuteAsync(IWorkflowInvoker invoker, WorkflowExecutionContext workflowContext, CancellationToken cancellationToken)
        {
            var activity = workflowContext.CurrentActivity;
        }
    }
}