using MVC002.BLL.Interfaces;
using MVC002.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC002.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork , IDisposable
    {
        private readonly AppDbContext _dbcontext;

        public IDepartmentRepository DepartmentRepository { get ; set ; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public UnitOfWork(AppDbContext dbcontext)
        {
            EmployeeRepository = new EmployeeRepository(dbcontext);
            DepartmentRepository = new DepartmentRepository(dbcontext);
            _dbcontext = dbcontext;
        }

        public async Task<int> SaveChangesCompleted()
        {
            return await _dbcontext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
         }
    }
}
