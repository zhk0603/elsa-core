using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Elsa.Dashboard.Web.Data;
using Elsa.Models;
using Elsa.Persistence;
using Elsa.Services;

namespace Elsa.Dashboard.Web.Pages.Approve
{
    public class HandleModel : PageModel
    {
        private readonly DemoDbContext _context;
        private readonly IWorkflowInvoker invoker;
        private readonly IWorkflowInstanceStore instanceStore;

        public HandleModel(DemoDbContext context, IWorkflowInvoker invoker, IWorkflowInstanceStore instanceStore)
        {
            _context = context;
            this.invoker = invoker;
            this.instanceStore = instanceStore;
        }

        public Data.Approve Approve { get; set; }

        [BindProperty] public Guid ApproveId { get; set; }

        [BindProperty] public bool IsPass { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id, bool pass)
        {
            Approve = await _context.Approves.FirstOrDefaultAsync(m => m.Id == id);

            if (Approve == null)
            {
                return NotFound();
            }

            ApproveId = id;
            IsPass = pass;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Approve = await _context.Approves.FirstOrDefaultAsync(m => m.Id == ApproveId);
            Approve.AuditStatus = IsPass ? (byte) 1 : (byte) 2;
            var leaveRecord = await _context.LeaveRecords.FirstOrDefaultAsync(x => x.Approve.Id == ApproveId);
            leaveRecord.Status = IsPass ? LeaveRecordStatusEnum.Pass : LeaveRecordStatusEnum.Reject;

            var instance = await instanceStore.GetByCorrelationIdAsync(ApproveId.ToString());

            var execContext = await invoker.ResumeAsync(instance, Variables.Empty,
                new[] {instance.BlockingActivities.FirstOrDefault()?.ActivityId});

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
