using MVC002.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC002.BLL.Interfaces
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
        IQueryable<Employee> GetByAddress(string address);
        IQueryable<Employee> GetByName(string name);
      
    }
}
