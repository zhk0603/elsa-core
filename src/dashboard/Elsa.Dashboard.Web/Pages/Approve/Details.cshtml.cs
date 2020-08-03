using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Elsa.Dashboard.Web.Data;

namespace Elsa.Dashboard.Web.Pages.Approve
{
    public class DetailsModel : PageModel
    {
        private readonly Elsa.Dashboard.Web.Data.DemoDbContext _context;

        public DetailsModel(Elsa.Dashboard.Web.Data.DemoDbContext context)
        {
            _context = context;
        }

        public Data.Approve Approve { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Approve = await _context.Approves.FirstOrDefaultAsync(m => m.Id == id);

            if (Approve == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
