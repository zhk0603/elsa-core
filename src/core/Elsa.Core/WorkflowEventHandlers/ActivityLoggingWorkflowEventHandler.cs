using Elsa.Models;
using Elsa.Services;
using Elsa.Services.Models;
using NodaTime;

namespace Elsa.WorkflowEventHandlers
{
    public class ActivityLoggingWorkflowEventHandler : WorkflowEventHandlerBase
    {
        private readonly IClock clock;

        public ActivityLoggingWorkflowEventHandler(IClock clock)
        {
            this.clock = clock;
        }

        protected override void ActivityExecuting(WorkflowExecutionContext workflowExecutionContext, IActivity activity)
        {
            var timeStamp = clock.GetCurrentInstant();
            workflowExecutionContext.Workflow.ExecutionActivities.Add(new ExecutionActivity(activity)
            {
                StartedAt = timeStamp
            });
        }

        protected override void ActivityExecuted(WorkflowExecutionContext workflowExecutionContext, IActivity activity)
        {
            var timeStamp = clock.GetCurrentInstant();
            workflowExecutionContext.Workflow.ExecutionLog.Add(
                new LogEntry(activity.Id, timeStamp, $"Successfully executed at {timeStamp}"));

            var executionActivity = workflowExecutionContext.GetActivityLastExecutionEntry(activity);
            if (executionActivity?.Status == ExecutionActivityStatus.Executing)
            {
                executionActivity.FinishedAt = timeStamp;
                executionActivity.Status = ExecutionActivityStatus.Finished;
                executionActivity.HandleStatus = ActivityHandleStatus.Normal;
            }
        }

        protected override void ActivityFaulted(
            WorkflowExecutionContext workflowExecutionContext,
            IActivity activity,
            string message)
        {
            var timeStamp = clock.GetCurrentInstant();
            workflowExecutionContext.Workflow.ExecutionLog.Add(
                new LogEntry(activity.Id, timeStamp, message, true));

            var executionActivity = workflowExecutionContext.GetActivityLastExecutionEntry(activity);
            if (executionActivity?.Status == ExecutionActivityStatus.Executing)
            {
                executionActivity.FaultedAt = timeStamp;
                executionActivity.Status = ExecutionActivityStatus.Faulted;
                executionActivity.HandleStatus = ActivityHandleStatus.Faulted;
            }
        }

        protected override void ActivityFallbacked(WorkflowExecutionContext workflowExecutionContext, IActivity activity)
        {
            var timeStamp = clock.GetCurrentInstant();
            workflowExecutionContext.Workflow.ExecutionLog.Add(
                new LogEntry(activity.Id, clock.GetCurrentInstant(), $"Successfully fallback at {timeStamp}"));

            var executionActivity = workflowExecutionContext.GetActivityLastExecutionEntry(activity);
            if (executionActivity?.Status == ExecutionActivityStatus.Executing)
            {
                executionActivity.FaultedAt = timeStamp;
                executionActivity.Status = ExecutionActivityStatus.Finished;
                executionActivity.HandleStatus = ActivityHandleStatus.Fallback;
            }
        }
    }
}