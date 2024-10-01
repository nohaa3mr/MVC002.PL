using AutoMapper;
using MVC002.DAL.Models;
using MVC002.PL.ViewModels;

namespace MVC002.PL.Profiles
{
    public class UserProfile :Profile
    {
        public UserProfile() : base()
        {
            CreateMap<UserViewModel, ApplicationUser>().ReverseMap();
        }
    }
}
