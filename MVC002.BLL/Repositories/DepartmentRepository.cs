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
    public class DepartmentRepository : IDepartmentRepository
    {
        private  readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;

        }
        public int Add(Department Entity)
        {
            _context.departments.Add(Entity);
            return _context.SaveChanges();
        }

        public int Delete(Department Entity)
        
        {
            _context.departments.Remove(Entity);
            return _context.SaveChanges();
        }

        public Department Get(int Id)
        {

        //    return _context.departments.FirstOrDefault( D=> D.Id == Id);
            return _context.departments.Find(Id);


        }

        public IEnumerable<Department> GetAll()
        {
            return _context.departments.ToList();
        }

        public int Update(Department Entity)
        {
            _context.departments.Update(Entity);
            return _context.SaveChanges();
        }
    }
}
