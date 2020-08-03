using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;

namespace Elsa.Activities.Permission.Results
{
    public class UnauthorizedResult : ActivityExecutionResult
    {
        public const string OutcomeName = "Unauthorized";

        public override Task ExecuteAsync(IWorkflowInvoker invoker, WorkflowExecutionContext workflowContext, CancellationToken cancellationToken)
        {
            var currentActivity = workflowContext.CurrentActivity;
            ScheduleNextActivities(workflowContext, new SourceEndpoint(currentActivity, OutcomeName));
            return Task.CompletedTask;
        }

        private void ScheduleNextActivities(WorkflowExecutionContext workflowContext, SourceEndpoint endpoint)
        {
            var completedActivity = workflowContext.CurrentActivity;
            var connections = workflowContext.Workflow.Connections
                .Where(x => x.Source.Activity == completedActivity &&
                            (x.Source.Outcome ?? OutcomeNames.Done).Equals(endpoint.Outcome,
                                StringComparison.OrdinalIgnoreCase)).ToArray();

            if (!connections.Any())
            {
                throw new InvalidOperationException("Unauthorized");
            }

            var activities = connections.Select(x => x.Target.Activity);

            workflowContext.ScheduleActivities(activities);
        }
    }
}
