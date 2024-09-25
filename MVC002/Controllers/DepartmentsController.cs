using Microsoft.AspNetCore.Mvc;
using MVC002.DAL.Models;
using MVC002.BLL.Interfaces;
using System;
using AutoMapper;
using MVC002.PL.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC002.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IUnitOfWork unitOfWork) //Dependency Injection to not throw null exception
        {
             _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult DeptHome()
        {
            var Departments = _unitOfWork.DepartmentRepository.GetAll();

            return View(Departments);
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> Create(Department department)
        {
            await _unitOfWork.DepartmentRepository.Add(department);
            int result = await _unitOfWork.SaveChangesCompleted();
            if (result > 0)
            {
                TempData["Message"] = "Department Is Created.";
            }
            else
            { return RedirectToAction("DeptHome"); }

            return  View(department);
           
        }

             
             
     
        public async Task<IActionResult> Details(int? id , string ViewName = "Details")
        {
            if (id is null)
            return  BadRequest(); 
            var department  = await _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department is null) return NotFound();

            return View(ViewName,department);
        }
        [HttpGet]
        public async Task< IActionResult> Delete(int? id)
        {
            //if (id is null) 
            // return  BadRequest();
            //var department =  _unitOfWork.DepartmentRepository.GetById(id.Value);
            //if (department is null)
            //    return NotFound();
             return await Details(id,"Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete([FromRoute] int? id, Department model)
        {

            try
            {
                if (id != model.Id) 
                   return BadRequest();
                if (ModelState.IsValid)
                {
                      _unitOfWork.DepartmentRepository.Delete(model) ;
                    await  _unitOfWork.SaveChangesCompleted();
                       return   RedirectToAction("DeptHome");
                   
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }




            return  View(model);
        }
        [HttpGet]
        public async Task<IActionResult>  Edit(int? id)
        {
           // if (id is null) 
           //return BadRequest();
           // var department = await _unitOfWork.DepartmentRepository.GetById(id.Value);
           // if (department is null)  
           //  return NotFound();
            return await Details( id , "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, Department model)
        {

            try
            {
                if (id != model.Id) 
                 return BadRequest();
             
                _unitOfWork.DepartmentRepository.Update(model);
              await   _unitOfWork.SaveChangesCompleted();
                        return RedirectToAction(nameof(DeptHome));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }

            return View(model);
        }

    }
}
