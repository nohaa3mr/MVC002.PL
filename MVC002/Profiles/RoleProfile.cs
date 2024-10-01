using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MVC002.PL.ViewModels;

namespace MVC002.PL.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ForMember(x => x.RoleName , O => O.MapFrom(R => R.Name)).ReverseMap();
        }
    }
}
