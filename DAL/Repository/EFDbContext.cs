using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("EmployeeDBEntities")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmpSalary> EmpSalary { get; set; }
    }
}
