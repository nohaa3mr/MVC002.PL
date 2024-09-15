﻿using MVC002.BLL.Interfaces;
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
        public int Add(Department department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges();
        }

        public int Delete(Department department)
        
        {
            _context.Departments.Remove(department);
            return _context.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department GetById(int Id)
        {
            return _context.Departments.Where(x=> x.Id ==Id).FirstOrDefault();
        }

        public int Update(Department department)
        {
            _context.Departments.Update(department);
            return _context.SaveChanges();
        }
    }
}
