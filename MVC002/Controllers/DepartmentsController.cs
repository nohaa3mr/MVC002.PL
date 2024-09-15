﻿using Microsoft.AspNetCore.Mvc;
using MVC002.DAL.Models;
using MVC002.BLL.Repositories;
using MVC002.BLL.Interfaces;

namespace MVC002.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentsController(IDepartmentRepository departmentRepository ) //Dependency Injection to not throw null exception
        {
            _repository =departmentRepository;
        }
        public IActionResult Index()
        {
           var Department = _repository.GetAll();
            return View(Department);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
