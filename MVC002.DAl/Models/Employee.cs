﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC002.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Is Required.")]
        [MaxLength(50, ErrorMessage ="MaxLength is 50 charcters.")]
        public string Name { get; set; }
        [Range(20,40,ErrorMessage ="Range between 20 and 40.")]
        public int? Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-aZ-Z]{5,10}-[a-aZ-Z]{4,10}-[a-aZ-Z]{4,10}" , ErrorMessage ="format:(123-street-City-Country)")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public  decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public int ? DepartmentId { get; set; }  //FK
        [InverseProperty("Employees")]
        public Department Department { get; set; }


    }
}
