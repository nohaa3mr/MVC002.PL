using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MVC002.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC002.BLL.Interfaces
{
    public interface IDepartmentRepository :IGenericRepository<Department>
    {
       

    }
}
