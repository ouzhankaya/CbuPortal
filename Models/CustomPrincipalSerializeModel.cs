using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using MongoDB.Bson;

namespace CbuPortal.Models
{
    public class CustomPrincipalSerializeModel
    {
        public string id { get; set; }
        public string photo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}