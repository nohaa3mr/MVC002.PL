using Microsoft.AspNetCore.Mvc;
using MVC002.DAL.Models;
using MVC002.BLL.Interfaces;
using System;

namespace MVC002.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentsController(IDepartmentRepository departmentRepository) //Dependency Injection to not throw null exception
        {
            _repository = departmentRepository;

        }
        [HttpGet]
        public IActionResult DeptHome()
        {
            //ViewData: obj from dictionary type , transfer data from controller(Action) to its view.
            ViewData["Message"] = "Hello From ViewData.";

            //ViewData: obj from dynamic type , transfer data from controller(Action) to its view.
            ViewBag.Message = "Hello From ViewBag.";


            var departments = _repository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (ModelState.IsValid) //server side validation
            {
                var Count = _repository.Add(dept);
                if (Count > 0)
                {
                  TempData["Message"] = "Department Is Created.";

                    return RedirectToAction(nameof(DeptHome));
                }
            }

            return View(dept);
        }
        public IActionResult Details(int? id , string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); //Invaild //Return Status Code 
            var department = _repository.GetById(id.Value);
            if (department is null) return NotFound();

            return View(ViewName,department);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            var department = _repository.GetById(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, Department model)
        {

            try
            {
                if (id != model.Id) 
                    return BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = _repository.Delete(model);
                    if (Count > 0)
                    {
                       return RedirectToAction("DeptHome");
                    }

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }




            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) 
           return BadRequest();
            var department = _repository.GetById(id.Value);
            if (department is null)  
             return NotFound();
            return Details( id , "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, Department model)
        {

            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = _repository.Update(model);
                    if (Count > 0)
                    {
                        return RedirectToAction("DeptHome");
                    }

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }




            return View(model);
        }

    }
}
