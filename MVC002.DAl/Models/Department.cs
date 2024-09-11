using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC002.DAL.Models
{
    internal class Department
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } 
        [Required]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;


    }
}
