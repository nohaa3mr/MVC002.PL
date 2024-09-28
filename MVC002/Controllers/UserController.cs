using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC002.DAL.Data.Migrations;
using System.Linq;
using MVC002.PL.ViewModels;
using NuGet.Packaging.Signing;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
namespace MVC002.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> user )
        {
            _usermanager = user;
        }


        public async Task<IActionResult> Index(string SearchValue)
        {
          var user=  Enumerable.Empty<UserViewModel>();
            if (string.IsNullOrEmpty(SearchValue))
            {
                user = _usermanager.Users.Select(U => new UserViewModel() {});
               
                return View();
            }
            else
            { var users = await _usermanager.FindByEmailAsync(SearchValue); 

            }

            return View();
        }
    }
}
