using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MVC002.DAL.Models;
using MVC002.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC002.PL.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager , IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async  Task<IActionResult> Index(string searchvalue )
        {
            if (string.IsNullOrEmpty(searchvalue))
            {
              var Roles = await  _roleManager.Roles.ToListAsync();
                var MappedRole = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(Roles);
                return View(MappedRole);
            }
            else 
            {
                var Role = await _roleManager.FindByNameAsync(searchvalue);
                var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);
                return View(new List<RoleViewModel> { mappedRole });
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {

                var maPPedRole = _mapper.Map<RoleViewModel, IdentityRole>(model);
                await _roleManager.CreateAsync(maPPedRole);
                return RedirectToAction("Index");
            }
            else
                return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> Details(string Id, string ViewName = "Details")
        {
            if (Id is null)
                return BadRequest();


            var Users = await _roleManager.FindByIdAsync(Id);
            if (Users is null)
                return NotFound();

            var Mapped = _mapper.Map<IdentityRole, RoleViewModel>(Users);
            return View(ViewName, Mapped);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model, [FromRoute] string Id)
        {
            if (Id != model.Id)
                return BadRequest();

            try
            {
                var role = await _roleManager.FindByIdAsync(Id);
                role.Name = model.RoleName;
             
                await _roleManager.UpdateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoleViewModel model, string id)
        {

            if (id != model.Id)
                return BadRequest();

            try
            {
                var roles = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(roles);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error" , "Home");
            }

        }

    }
}
