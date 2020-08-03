using System;
using System.Linq;
using System.Threading.Tasks;
using Elsa.Dashboard.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Elsa.Dashboard.Web.Pages.Leave
{
    public class EditModel : PageModel
    {
        private readonly Elsa.Dashboard.Web.Data.DemoDbContext _context;

        public EditModel(Elsa.Dashboard.Web.Data.DemoDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LeaveRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveRecordExists(LeaveRecord.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LeaveRecordExists(Guid id)
        {
            return _context.LeaveRecords.Any(e => e.Id == id);
        }
    }
}
