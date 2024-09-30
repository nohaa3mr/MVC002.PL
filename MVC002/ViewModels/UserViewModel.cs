using MVC002.DAL.Models;
using System.Collections.Generic;

namespace MVC002.PL.ViewModels
{
    public class UserViewModel 
    {
        public string Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }

    }
}
