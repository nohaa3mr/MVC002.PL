using AutoMapper;
using MVC002.DAL.Models;
using MVC002.PL.ViewModels;

namespace MVC002.PL.Profiles
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile():base()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
