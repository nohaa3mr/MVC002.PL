using Microsoft.AspNetCore.Mvc;
using MVC002.BLL.Interfaces;
using MVC002.DAL.Models;
using System;

namespace MVC002.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository) //Ask CLR to create an obj  from a class implements IEmployeeRepository interface.
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            ViewBag.Message = "Hello From ViewBag";
            ViewData["Message"] = "Hello From ViewData";
            var employees = _employeeRepository.GetAll();

            return View(employees);
        }


        [HttpGet]
        public IActionResult Create()
        {
             ViewBag.Departments= _departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Employee employee)
        {
            
                var count = _employeeRepository.Add(employee);
               if (count > 0)
               {
                TempData["Message"] = "New Employee Is Created.";

                 return RedirectToAction(nameof(Index));

               }

            
            return View(employee);
        }


        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); //Invaild //Return Status Code 
            var employee = _employeeRepository.GetById(id.Value);
            if (employee is null) return NotFound();

            return View(ViewName, employee);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            var employee= _employeeRepository.GetById(id.Value);
            if (employee is null)
                return NotFound();
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, Employee employee)
        {

            try
            {
                if (id != employee.Id)
                    return BadRequest();
                //if (ModelState.IsValid)
                //{
                   var Count = _employeeRepository.Delete(employee);
                    if (Count > 0)
                    {
                        return RedirectToAction("Index");
                    }

               // }
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
            if (id is null)
                return BadRequest();
            var employee = _employeeRepository.GetById(id.Value);
            if (employee is null)
                return NotFound();
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( Employee employee)
        {
            try
            {
                    var Count = _employeeRepository.Update(employee);
                    if (Count > 0)
                    {
                        return RedirectToAction("Create");
                    }

                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }

            return View(employee);
        }

    }
}

