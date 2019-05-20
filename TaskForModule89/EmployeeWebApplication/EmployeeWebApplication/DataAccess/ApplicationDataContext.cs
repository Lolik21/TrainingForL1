using EmployeeWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebApplication.DataAccess
{
    public sealed class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AccountInfo> AccountInfos { get; set; }
    }
}