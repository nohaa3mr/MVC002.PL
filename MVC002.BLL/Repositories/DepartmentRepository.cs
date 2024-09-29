using MVC002.BLL.Interfaces;
using MVC002.DAL.Data;
using MVC002.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC002.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext dbContext) : base(dbContext) //Ask CLR to create an obj to open the connection for database.
        { 
        
        
        }
      

    }
}
