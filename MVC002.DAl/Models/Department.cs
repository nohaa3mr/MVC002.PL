using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC002;
namespace MVC002.DAL.Models
{
    public class Department
    { 
        public int Id { get; set; }
        [Required(ErrorMessage ="Code Is Required")]
        public string Code { get; set; } 
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        [InverseProperty("Department")]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
