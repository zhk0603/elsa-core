using System;
using System.Threading.Tasks;
using Elsa.Dashboard.Web.Data;
using Elsa.Models;
using Elsa.Persistence;
using Elsa.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elsa.Dashboard.Web.Pages.Leave
{
    public class CreateModel : PageModel
    {
        private readonly Elsa.Dashboard.Web.Data.DemoDbContext _context;
        private readonly IWorkflowInvoker invoker;
        private readonly IWorkflowDefinitionStore definitionStore;

        public CreateModel(DemoDbContext context,
            IWorkflowInvoker invoker,
            IWorkflowDefinitionStore definitionStore)
        {
            _context = context;
            this.invoker = invoker;
            this.definitionStore = definitionStore;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty] public LeaveRecord LeaveRecord { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var approve = new Data.Approve()
            {
                Id = Guid.NewGuid(),
            };

            var definition = await definitionStore.GetByIdAsync("9c928b13905743e6a0fb0d075e48213b", VersionOptions.Latest);
            var variable = new Variables();
            variable.SetVariable("Model", LeaveRecord);
            await invoker.StartAsync(definition, variable, correlationId: approve.Id.ToString());

            LeaveRecord.Id = Guid.NewGuid();
            approve.CorrelateId = LeaveRecord.Id;
            LeaveRecord.Approve = approve;
            _context.LeaveRecords.Add(LeaveRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
