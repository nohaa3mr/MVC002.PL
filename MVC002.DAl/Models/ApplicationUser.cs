﻿using Microsoft.AspNetCore.Identity;
using MVC002.DAL.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC002.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        public bool IsAgree { get; set; }
    }  
}
