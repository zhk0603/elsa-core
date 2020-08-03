using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elsa.Dashboard.Web.Data
{
    public class Approve
    {
        public Guid Id { get; set; }
        public int WfInstanceId { get; set; }
        public int WfActivityInstanceId { get; set; }
   
        //0：待审核 1： 通过 2： 不通过
        public byte AuditStatus { get; set; }
        public ApproveTypeEnum Type { get; set; }
        public Guid CorrelateId { get; set; } // 关联业务对象id.
    }
}
