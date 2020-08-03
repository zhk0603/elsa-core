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
    public class IndexModel : PageModel
    {
        private readonly Elsa.Dashboard.Web.Data.DemoDbContext _context;

        public IndexModel(Elsa.Dashboard.Web.Data.DemoDbContext context)
        {
            _context = context;
        }

        public IList<Data.Approve> Approve { get;set; }

        public async Task OnGetAsync()
        {
            Approve = await _context.Approves.ToListAsync();
        }
    }
}
