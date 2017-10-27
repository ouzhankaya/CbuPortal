using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
//using MongoDB.Bson;

namespace CbuPortal.Models
{
    public class CustomPrincipalHelper : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string KULLANICI_ROL)
        {
            return true; 
        }

        public CustomPrincipalHelper(string KULLANICI)
        {
            this.Identity = new GenericIdentity(KULLANICI);
        }

        public string id { get; set; }
        public string photo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}