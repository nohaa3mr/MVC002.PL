using Microsoft.EntityFrameworkCore;
using MVC002.BLL.Interfaces;
using MVC002.DAL.Data;
using MVC002.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MVC002.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository 
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext) : base(dbContext) //Allow Dependency Injection 
        {
            _dbContext = dbContext;
        }

        

        public IQueryable<Employee> GetByAddress(string address)
        {
            return _dbContext.Employees.Where(emp => emp.Address == address);
        }

        public IQueryable<Employee> GetByName(string name)
        {
            return _dbContext.Employees.Where(e => e.Name.ToLower().Contains(name.ToLower()));
        }

        public Task<int> SaveChangesCompleted()
        {
            throw new NotImplementedException();
        }
    }
}

