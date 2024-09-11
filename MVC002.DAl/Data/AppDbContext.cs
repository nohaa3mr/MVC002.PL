using Microsoft.EntityFrameworkCore;
using MVC002.DAL.Configuation;
using MVC002.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC002.DAL.Data
{
    internal class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Server = . ; Database = MVCApp; Trusted_Connection = true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly( Assembly.GetExecutingAssembly());
        }
        public DbSet<Department> departments  { get; set; }
    }
}
