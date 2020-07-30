using Elsa.Services.Models;
using NodaTime;

namespace Elsa.Models
{
    public class ExecutionActivity
    {
        public ExecutionActivity(IActivity activity)
        {
            ActivityId = activity.Id;
            ActivityType = activity.Type;
        }

        public string ActivityId { get; set; }
        public string ActivityType { get; set; }
        public Instant StartedAt { get; set; }
        public Instant? FinishedAt { get; set; }
        public Instant? FaultedAt { get; set; }
        public ExecutionActivityStatus Status { get; set; }
        public ActivityHandleStatus HandleStatus { get; set; }
    }

    public enum ExecutionActivityStatus
    {
        Executing,
        Finished,
        Faulted,
    }

    public enum ActivityHandleStatus
    {
        /// <summary>
        /// 还在执行中，未产生结果。
        /// </summary>
        Executing,

        Faulted,

        /// <summary>
        /// 正常结束
        /// </summary>
        Normal,

        /// <summary>
        /// 环节回退。
        /// </summary>
        Fallback
    }
}
