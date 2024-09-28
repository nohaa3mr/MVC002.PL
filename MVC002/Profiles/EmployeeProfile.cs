using AutoMapper;
using MVC002.DAL.Data;
using MVC002.DAL.Models;
using MVC002.PL.ViewModels;

namespace MVC002.PL.Profiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
