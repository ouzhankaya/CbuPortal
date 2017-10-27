using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CbuPortal.Models
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        protected virtual new CustomPrincipalHelper CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipalHelper; }

        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = filterContext.HttpContext.User as CustomPrincipalHelper;
                filterContext.Result = new RedirectResult("~/Login/Login");
                return;
            }
           
        }
    }
}
