using Microsoft.EntityFrameworkCore;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _app;

        public GenericRepository( AppDbContext app)
        {
            _app = app;
        }
        public int Add(T entity)
        {
              _app.Add(entity);
            return _app.SaveChanges();
        }

        public int Delete(T entity)
        {
            _app.Remove(entity);
            return _app.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
               if(typeof(T) == typeof(Employee)) 
               {
               return (IEnumerable<T>)_app.Employees.Include( e=>e.Department).ToList();

               }
            return _app.Set<T>().ToList();
        }

        public T GetById(int id)
            => _app.Set<T>().Find(id);
        

        public int Update(T entity)
        {
            _app.Update(entity);
            return _app.SaveChanges();
               
        }
    }
}
