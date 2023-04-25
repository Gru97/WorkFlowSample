using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WFE.Console.TransferMoneyExample.Domain;
using WorkflowCore.Persistence.SqlServer;

namespace WFE.Console.TransferMoneyExample.Infra
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Property(e => e.Owner).HasMaxLength(500);
            modelBuilder.Entity<Account>().HasData(new List<Account>()
            {
                new Account("Gholi","1258823",200),
                new Account("Taghi","9852333",200),
                new Account("Kokab","5245688",200),
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
