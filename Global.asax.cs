using CbuPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace CbuPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                try
                {
                    var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                    var newUser = new CustomPrincipalHelper(authTicket.Name);

                    newUser.id = serializeModel.id;
                    newUser.photo = serializeModel.photo;
                    newUser.Firstname = serializeModel.Firstname;
                    newUser.Lastname = serializeModel.Lastname;
                    newUser.Username = serializeModel.Username;
                    newUser.Password = serializeModel.Password;

                    HttpContext.Current.User = newUser;
                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    authCookie.Expires = DateTime.MaxValue;
                    Response.AppendCookie(authCookie);
                }
                catch (Exception ex)
                {
                    FormsAuthentication.SignOut();
                    Response.Clear();
                    Response.Redirect("~/Login/Login");
                }
            }


        }
    }
}
