using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MVC002.PL.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ApplicationUser = MVC002.DAL.Models.ApplicationUser;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Authorization;

namespace MVC002.PL.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> user  , IMapper mapper )
        {
            _usermanager = user;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string SearchValue)
        {
          var users=  Enumerable.Empty<UserViewModel>();
            if (string.IsNullOrEmpty(SearchValue))
            {
                users = (IEnumerable<UserViewModel>)await _usermanager.Users.Select( U => new UserViewModel()
                {    Id = U.Id,
                    Email = U.Email,
                    FName = U.FName,
                    LName = U.LName,
                    PhoneNumber = U.PhoneNumber,
                    Roles =  _usermanager.GetRolesAsync(U).Result

                }).ToListAsync();
                return View(users);
            }
            else
            { var Users = await _usermanager.FindByEmailAsync(SearchValue);
                var MappedUser = new UserViewModel()
                {
                    Id = Users.Id,
                    Email = Users.Email,
                    FName = Users.FName,
                    LName = Users.LName,
                    PhoneNumber = Users.PhoneNumber,
                    Roles = _usermanager.GetRolesAsync(Users).Result
                };
                return View(new List<UserViewModel> { MappedUser});
            }
        }
       
        [HttpGet]
        public async Task<IActionResult> Details(string Id , string ViewName = "Details")
        { if (Id is null)
         return BadRequest();


           var Users=await _usermanager.FindByIdAsync(Id);
            if (Users is null)
                return NotFound();

         var Mapped=   _mapper.Map<ApplicationUser, UserViewModel>(Users);
            return View( ViewName, Mapped);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model ,[FromRoute] string Id)
        {
            if (Id != model.Id)
            return BadRequest();

                try
                {
                   var User =await _usermanager.FindByIdAsync(Id);
                   User.LName = model.LName;
                  User.FName = model.FName;
                User.PhoneNumber = model.PhoneNumber;
                   await _usermanager.UpdateAsync(User);
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
        public async Task<IActionResult> ConfirmDelete( UserViewModel model , string id)
        {

            if (id != model.Id)
                return BadRequest();

            try
            {
                var User = await _usermanager.FindByIdAsync(id);
                await _usermanager.DeleteAsync(User);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error");
            }


        }

    }
}
