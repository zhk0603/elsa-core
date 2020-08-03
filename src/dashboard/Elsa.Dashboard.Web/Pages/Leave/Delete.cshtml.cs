using System;
using System.Threading.Tasks;
using Elsa.Dashboard.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Elsa.Dashboard.Web.Pages.Leave
{
    public class DeleteModel : PageModel
    {
        private readonly Elsa.Dashboard.Web.Data.DemoDbContext _context;

        public DeleteModel(Elsa.Dashboard.Web.Data.DemoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LeaveRecord LeaveRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveRecord = await _context.LeaveRecords.FirstOrDefaultAsync(m => m.Id == id);

            if (LeaveRecord == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveRecord = await _context.LeaveRecords.FindAsync(id);

            if (LeaveRecord != null)
            {
                _context.LeaveRecords.Remove(LeaveRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
