using CbuPortal.Models;
using CbuPortal.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CbuPortal.LoginService;
using CbuPortal.AccountService;
using Newtonsoft.Json;
using System.Web.Security;

namespace CbuPortal.Controllers
{
    public class LoginController : BaseController
    {
       
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

       [HttpPost]
       public ActionResult Login(UserVM user)
        {
             LoginClient AccSer = new LoginClient();
            tblLoginInformations CurrentUser = AccSer.userLogin(user.userName, user.password);
            if(CurrentUser!=null)
            {
                var serializeModel = new CustomPrincipalSerializeModel
                {
                    id = CurrentUser.user._id.ToString(),
                    photo = CurrentUser.profile.Avatar,//TODO: Serviste Profil ile beraber çek
                    Firstname = CurrentUser.user.UserName,//Servisten Gelecek
                    Lastname = CurrentUser.user.UserSurname,//Servisten Gelecek 
                    Username = CurrentUser.user.Email,
                    Password = CurrentUser.user.UserPassword,
                };
                string userData = JsonConvert.SerializeObject(serializeModel);
                #region Kullanıcı 180dk sonra loginden düşmesi için tutulan cookie

                var authTicket = new FormsAuthenticationTicket(
                         1,
                         CurrentUser.user.Email,
                         DateTime.Now,
                         DateTime.Now.AddMinutes(120),
                         true,
                         userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.Expires = DateTime.MaxValue;
                Response.Cookies.Add(cookie);
                #endregion
                return RedirectToAction("Home", "Home");
            }
            else
            {
                ViewBag.Error = "Hatalı Kullanıcıadı veya Şifre ";
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Response.Clear();
            return RedirectToAction("Login", "Login");
        }


        [HttpPost]
        public ActionResult CreateUser(CreateUserVM d)
        {
            AccountClient Client = new AccountService.AccountClient();
            tblUser user = new tblUser();
            tblProfile profile = new tblProfile();
            user.Email = d.Email;
            user.UserName = d.UserName;
            user.UserPassword = d.Password;
            user.UserSurname = d.Surname;

            tblLoginInformations CurrentUser = Client.createUser(user, profile);
            if (CurrentUser != null)
            {
                var serializeModel = new CustomPrincipalSerializeModel
                {
                    id = CurrentUser.user._id.ToString(),
                    photo = CurrentUser.profile.Avatar,//TODO: Serviste Profil ile beraber çek
                    Firstname = CurrentUser.user.UserName,//Servisten Gelecek
                    Lastname = CurrentUser.user.UserSurname,//Servisten Gelecek 
                    Username = CurrentUser.user.Email,
                    Password = CurrentUser.user.UserPassword,
                };
                string userData = JsonConvert.SerializeObject(serializeModel);
                #region Kullanıcı 180dk sonra loginden düşmesi için tutulan cookie

                var authTicket = new FormsAuthenticationTicket(
                         1,
                         CurrentUser.user.Email,
                         DateTime.Now,
                         DateTime.Now.AddMinutes(120),
                         true,
                         userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.Expires = DateTime.MaxValue;
                Response.Cookies.Add(cookie);
                #endregion
                return RedirectToAction("Home", "Home");
            }
            return View("Login");
        }

    }
}