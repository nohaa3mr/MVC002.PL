using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC002.BLL.Interfaces;
using MVC002.BLL.Repositories;
using MVC002.DAL.Data;
using MVC002.DAL.Data.Migrations;
using MVC002.DAL.Models;
using MVC002.PL.Profiles;
using System.Collections.Generic;
using ApplicationUser = MVC002.DAL.Models.ApplicationUser;


namespace MVC002
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);

            Builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            Builder.Services.AddDbContext<AppDbContext>(Options => { Options.UseSqlServer(Builder.Configuration.GetConnectionString("DefaultConnection")); });
            Builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); //Allow DI for DepartmentRepository
            Builder.Services.AddAutoMapper(M => M.AddProfiles(new List<Profile>() { new UserProfile(), new DepartmentProfile(), new EmployeeProfile()  , new RoleProfile()}));
            Builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;
                Options.Password.RequireDigit = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
             Builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            { options.AccessDeniedPath = "Home/Error"; 
                options.LoginPath = "Account/Login";
            });
          var app=  Builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Register}/{id?}");
            });

            app.Run();


        }



    }
}
