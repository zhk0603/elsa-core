using System;

namespace Elsa.Dashboard.Web.Data
{
    public class LeaveRecord
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public int Days { get; set; }
        public LeaveRecordStatusEnum Status { get; set; }
        public Approve Approve { get; set; }
    }
}