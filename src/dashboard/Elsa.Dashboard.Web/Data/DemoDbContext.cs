using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elsa.Persistence.EntityFrameworkCore.DbContexts;

namespace Elsa.Dashboard.Web.Data
{
    public class DemoDbContext : DbContext
    {
        public DbSet<Approve> Approves { get; set; }
        public DbSet<LeaveRecord> LeaveRecords { get; set; }

        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }
    }
}
