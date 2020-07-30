using System;
using System.Collections.Generic;
using System.Text;
using Elsa.Models;

namespace Elsa.Persistence.EntityFrameworkCore.Entities
{
    public class ExecutionActivityEntity
    {
        public int Id { get; set; }
        public WorkflowInstanceEntity WorkflowInstance { get; set; }
        public string ActivityId { get; set; }
        public string ActivityType { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public DateTime? FaultedAt { get; set; }
        public ExecutionActivityStatus Status { get; set; }
        public ActivityHandleStatus HandleStatus { get; set; }
    }
}
