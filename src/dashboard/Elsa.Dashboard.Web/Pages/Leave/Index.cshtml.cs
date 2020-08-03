using System.Collections.Generic;
using System.Threading.Tasks;
using Elsa.Dashboard.Web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Elsa.Dashboard.Web.Pages.Leave
{
    public class IndexModel : PageModel
    {
        private readonly Elsa.Dashboard.Web.Data.DemoDbContext _context;

        public IndexModel(Elsa.Dashboard.Web.Data.DemoDbContext context)
        {
            _context = context;
        }

        public IList<LeaveRecord> LeaveRecord { get;set; }

        public async Task OnGetAsync()
        {
            LeaveRecord = await _context.LeaveRecords.ToListAsync();
        }
    }
}
