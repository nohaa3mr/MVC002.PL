using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVC002.BLL.Interfaces;
using MVC002.DAL.Models;
using MVC002.PL.ViewModels;
using System;
using System.Collections.Generic;

namespace MVC002.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository ,IMapper mapper) //Ask CLR to create an obj  from a class implements IEmployeeRepository interface.
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
                _mapper = mapper;
        }
        public IActionResult Index()
        {
           var employees = _employeeRepository.GetAll();
            var MappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);


            return View(MappedEmp);
        }


        [HttpGet]
        public IActionResult Create()
        {
             ViewBag.Departments= _departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            #region Manual Mapping
            //Mapping
            //1.Manual Mapping
            //Employee employee = new Employee()
            //{
            //    Name= employeeVM.Name,
            //    Address = employeeVM.Address,
            //    Age = employeeVM.Age,
            //    Salary = employeeVM.Salary,
            //    Email = employeeVM.Email
            //};
            //1.2.Explicit Casting

            #endregion

            //2.Auto Mapping -> install package [auto mapper]
            var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                var count = _employeeRepository.Add(MappedEmployee); //Invalid
               if (count > 0)
               {
                //TempData["Message"] = "New Employee Is Created.";

                 return RedirectToAction(nameof(Index));

               }

            
            return View(employeeVM);
        }


        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); //Invaild //Return Status Code 
           //Invalid
            var employee = _employeeRepository.GetById(id.Value);
            if (employee is null) 
               return NotFound();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
          

            return View(ViewName, MappedEmployee);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            var employee= _employeeRepository.GetById(id.Value);
            if (employee is null)
                return NotFound();
            return Details(id , "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, EmployeeViewModel employee)
        {

            try
            {

                if (id != employee.Id)
                    return BadRequest();

                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);

                var Count = _employeeRepository.Delete(MappedEmployee);
                    if (Count > 0)
                    {
                        return RedirectToAction("Index");
                    }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }




            return View(employee);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null)
            //    return BadRequest();
            //var employee = _employeeRepository.GetById(id.Value);
            //ViewBag.Departments = _departmentRepository.GetAll();

            //if (employee is null)
            //{
            //    return NotFound();
            //}

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( EmployeeViewModel employee , [FromRoute]int?id)
        {
            if (id != employee.Id)
                return BadRequest();
            if (employee is null)
                return NotFound();
            var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);
                _employeeRepository.Update(MappedEmployee);
            return View(employee);
        }

    }
}

