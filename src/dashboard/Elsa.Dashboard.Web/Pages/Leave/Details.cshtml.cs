using System;
using System.Threading.Tasks;
using Elsa.Dashboard.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Elsa.Dashboard.Web.Pages.Leave
{
    public class DetailsModel : PageModel
    {
        private readonly Elsa.Dashboard.Web.Data.DemoDbContext _context;

        public DetailsModel(Elsa.Dashboard.Web.Data.DemoDbContext context)
        {
            _context = context;
        }

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
    }
}
