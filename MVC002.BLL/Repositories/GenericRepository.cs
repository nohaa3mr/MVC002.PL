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

        public GenericRepository(AppDbContext app)
        {
            _app = app;
        }
        public async Task Add(T entity)
        {
          await  _app.AddAsync(entity);
        }

        public void Delete(T entity)
        {
             _app.Remove(entity);
        }
        
        public async Task<IEnumerable<T>> GetAll()
        {
               if(typeof(T) == typeof(Employee)) 
               {
               return (IEnumerable<T>) await _app.Employees.Include( e=>e.Department).ToListAsync();

               }
            return await _app.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
            =>   await _app.Set<T>().FindAsync(id);
        

        public  void Update(T entity)
        {
            _app.Update(entity);
               
        }
    }
}
