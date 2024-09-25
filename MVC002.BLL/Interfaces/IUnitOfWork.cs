using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC002.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public Task<int> SaveChangesCompleted();
    }
}
