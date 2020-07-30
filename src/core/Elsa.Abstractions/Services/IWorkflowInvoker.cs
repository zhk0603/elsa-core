using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Models;
using Elsa.Services.Models;
using Newtonsoft.Json.Linq;

namespace Elsa.Services
{
    public interface IWorkflowInvoker
    {
        Task<WorkflowExecutionContext> StartAsync(
            Workflow workflow,
            IEnumerable<IActivity> startActivities = default,
            CancellationToken cancellationToken = default
        );
        
        Task<WorkflowExecutionContext> StartAsync<T>(
            Variables input = default,
            IEnumerable<string> startActivityIds = default,
            string correlationId = default,
            CancellationToken cancellationToken = default) where T : IWorkflow, new();
        
        Task<WorkflowExecutionContext> StartAsync(
            WorkflowDefinitionVersion workflowDefinition,
            Variables input = default,
            IEnumerable<string> startActivityIds = default,
            string correlationId = default,
            CancellationToken cancellationToken = default
        );

        Task<WorkflowExecutionContext> ResumeAsync(
            Workflow workflow,
            IEnumerable<IActivity> startActivities = default,
            CancellationToken cancellationToken = default
        );

        Task<WorkflowExecutionContext> ResumeAsync<T>(
            WorkflowInstance workflowInstance,
            Variables input = default,
            IEnumerable<string> startActivityIds = default,
            CancellationToken cancellationToken = default
        ) where T : IWorkflow, new();

        Task<WorkflowExecutionContext> ResumeAsync(
            WorkflowInstance workflowInstance,
            Variables input = default,
            IEnumerable<string> startActivityIds = default,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// 从阻断的 activity 回退至流程之前执行过的 activity
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="blockingActivities"></param>
        /// <param name="historyActivities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WorkflowExecutionContext> FallbackAsync(
            Workflow workflow,
            IEnumerable<IActivity> blockingActivities = default,
            IEnumerable<IActivity> historyActivities = default,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// 从阻断的 activity 回退至流程之前执行过的 activity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workflowInstance"></param>
        /// <param name="input"></param>
        /// <param name="blockingActivityIds"></param>
        /// <param name="historyActivityIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WorkflowExecutionContext> FallbackAsync<T>(
            WorkflowInstance workflowInstance,
            Variables input = default,
            IEnumerable<string> blockingActivityIds = default,
            IEnumerable<string> historyActivityIds = default,
            CancellationToken cancellationToken = default
        ) where T : IWorkflow, new();

        /// <summary>
        /// 从阻断的 activity 回退至流程之前执行过的 activity
        /// </summary>
        /// <param name="workflowInstance"></param>
        /// <param name="input"></param>
        /// <param name="blockingActivityIds"></param>
        /// <param name="historyActivityIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WorkflowExecutionContext> FallbackAsync(
            WorkflowInstance workflowInstance,
            Variables input = default,
            IEnumerable<string> blockingActivityIds = default,
            IEnumerable<string> historyActivityIds = default,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Starts new workflows that start with the specified activity name and resumes halted workflows that are blocked on activities with the specified activity name.
        /// </summary>
        Task<IEnumerable<WorkflowExecutionContext>> TriggerAsync(
            string activityType,
            Variables input = default,
            string correlationId = default,
            Func<JObject, bool> activityStatePredicate = default,
            CancellationToken cancellationToken = default);
    }
}