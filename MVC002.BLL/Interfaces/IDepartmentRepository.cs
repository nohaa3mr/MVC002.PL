using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MVC002.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC002.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> GetAll();

        Department Get(int Id);

        int Add(Department Entity);
        int Update(Department Entity);
        int Delete(Department Entity);

    }
}
