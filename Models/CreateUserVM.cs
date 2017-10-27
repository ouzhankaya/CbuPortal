using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CbuPortal.Models
{
    public class CreateUserVM
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }

    }
}