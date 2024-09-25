using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVC002.BLL.Interfaces;
using MVC002.BLL.Repositories;
using MVC002.DAL.Models;
using MVC002.PL.Helpers;
using MVC002.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC002.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork ,IMapper mapper) //Ask CLR to create an obj  from a class implements IEmployeeRepository interface.
        {
              _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
      
      public async Task<IActionResult> Index(string searchvalue)
      {
        IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(searchvalue))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAll();
            }

            else
                employees =  _unitOfWork.EmployeeRepository.GetByName(searchvalue);
            var MappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(MappedEmp);
      
       }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
             ViewBag.Departments= await _unitOfWork.EmployeeRepository.GetAll();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
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
                 employeeVM.ImageName=  DocumentSettings.UploadFile(employeeVM.Image, "Images");
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
              await  _unitOfWork.EmployeeRepository.Add(MappedEmployee);
              await  _unitOfWork.SaveChangesCompleted();
            
            TempData["Message"] = "New Employee Is Created.";

            return RedirectToAction(nameof(Index));
            


        }


        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); //Invaild //Return Status Code 
                                     //Invalid

            var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            _unitOfWork.SaveChangesCompleted();
            var MappedEmployee = _mapper.Map <EmployeeViewModel>(employee);

            if (employee is null) 
               return NotFound();
          

            return View(ViewName, MappedEmployee);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
          
            return Details(id , "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Delete([FromRoute] int? id, EmployeeViewModel employee)
        { 
                if (id != employee.Id)
                    return BadRequest();

                try
                {
                
                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);
                    _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                    var result=  _unitOfWork.SaveChangesCompleted().Result;
                      if(result >  0  &&  employee.ImageName != null)
                      {
                         DocumentSettings.DeleteFile(employee.ImageName, "Images");
                      }

                    return RedirectToAction("Index");
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                    return View(employee);
                }
            
            
           
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
            if (employee.Image is not null)
            {
                employee.ImageName = DocumentSettings.UploadFile(employee.Image, "Images");

            }
            var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);
            _unitOfWork.EmployeeRepository.Update(MappedEmployee);
            return View(employee);
        }

    }
}

