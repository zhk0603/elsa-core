using System.Threading;
using System.Threading.Tasks;
using Elsa.Services.Models;

namespace Elsa.Services
{
    public abstract class WorkflowEventHandlerBase : IWorkflowEventHandler
    {
        public Task ActivityExecutingAsync(
            WorkflowExecutionContext workflowExecutionContext,
            IActivity activity,
            CancellationToken cancellationToken
        )
        {
            ActivityExecuting(workflowExecutionContext, activity);
            return Task.CompletedTask;
        }

        public virtual Task ActivityExecutedAsync(
            WorkflowExecutionContext workflowExecutionContext,
            IActivity activity,
            CancellationToken cancellationToken
        )
        {
            ActivityExecuted(workflowExecutionContext, activity);
            return Task.CompletedTask;
        }

        public virtual Task ActivityFaultedAsync(
            WorkflowExecutionContext workflowExecutionContext,
            IActivity activity,
            string message,
            CancellationToken cancellationToken
        )
        {
            ActivityFaulted(workflowExecutionContext, activity, message);
            return Task.CompletedTask;
        }

        public Task ActivityFallbackedAsync(
            WorkflowExecutionContext workflowExecutionContext,
            IActivity activity,
            CancellationToken cancellationToken
        )
        {
            ActivityFallbacked(workflowExecutionContext, activity);
            return Task.CompletedTask;
        }


        public virtual Task InvokingHaltedActivitiesAsync(
            WorkflowExecutionContext workflowExecutionContext,
            CancellationToken cancellationToken
        )
        {
            InvokingHaltedActivities(workflowExecutionContext);
            return Task.CompletedTask;
        }

        public virtual Task WorkflowInvokedAsync(
            WorkflowExecutionContext workflowExecutionContext,
            CancellationToken cancellationToken
        )
        {
            WorkflowInvoked(workflowExecutionContext);
            return Task.CompletedTask;
        }

        protected virtual void ActivityFallbacked(WorkflowExecutionContext workflowExecutionContext, IActivity activity)
        {
        }

        protected virtual void ActivityExecuting(WorkflowExecutionContext workflowExecutionContext, IActivity activity)
        {
        }

        protected virtual void ActivityExecuted(WorkflowExecutionContext workflowExecutionContext, IActivity activity)
        {
        }

        protected virtual void ActivityFaulted(
            WorkflowExecutionContext workflowExecutionContext,
            IActivity activity,
            string message
        )
        {
        }

        protected virtual void InvokingHaltedActivities(WorkflowExecutionContext workflowExecutionContext)
        {
        }

        protected virtual void WorkflowInvoked(WorkflowExecutionContext workflowExecutionContext)
        {
        }
    }
}