using Microsoft.AspNetCore.Mvc;
using MVC002.DAL.Models;
using MVC002.BLL.Interfaces;
using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MVC002.Controllers
{ 

    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IUnitOfWork unitOfWork ) 
        {
             _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> DeptHome()
        {
            var Departments = await _unitOfWork.DepartmentRepository.GetAll();

            return  View(Departments);
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return  View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Create(Department department)
        {
           try
           { 
				await _unitOfWork.DepartmentRepository.Add(department);
				int result = await _unitOfWork.SaveChangesCompleted();

                TempData["Message"] = "New Department Is Created.";

					return RedirectToAction(nameof(DeptHome));
           }
		   catch(Exception ex)
		   {
				ModelState.AddModelError(string.Empty, ex.Message);
		   }

			return View(department);
        }

             
        public async Task<IActionResult> Details(int? id , string ViewName = "Details")
        {
            if (id is null)
            return  BadRequest();
            var department  = await _unitOfWork.DepartmentRepository.GetById(id.Value);

            if (department is null)
               return NotFound();

            return View(ViewName,department);
        }
        [HttpGet]
        public async Task< IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department is null)
                return NotFound();
            return await Details(id,"Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Department model,[FromRoute] int? id  )
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
            if (id is null)
                return BadRequest();
            var department = await _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department is null)
                return NotFound();
            return await Details( id , "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( Department model , [FromRoute] int? id)
        {

            try
            {
                if (id != model.Id) 
                 return BadRequest();
                if(model != null)
                {
					_unitOfWork.DepartmentRepository.Update(model);
					await _unitOfWork.SaveChangesCompleted();
					return RedirectToAction(nameof(DeptHome));
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
