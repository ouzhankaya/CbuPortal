using CbuPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CbuPortal.Controllers
{
    public class BaseController : Controller
    {


        protected virtual new CustomPrincipalHelper User
        {

            get { return HttpContext.User as CustomPrincipalHelper; }


        }
    }
}