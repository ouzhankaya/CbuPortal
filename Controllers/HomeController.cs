using CbuPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CbuPortal.FindUser;
using MongoDB.Bson;
using CbuPortal.Entity.Models;
using CbuPortal.NotificationEvents;
namespace CbuPortal.Controllers
{
    [CustomAuthorize]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Home()
        {
         
            ViewBag.Avatar = User.photo;
            ViewBag.FullName = User.Firstname + " " + User.Lastname;
            ViewBag.id = User.id;
            ViewBag.ActiveFullName = User.Firstname + " " + User.Lastname;
            ViewBag.Activeid = User.id; ;
            ViewBag.ActiveAvatar = User.photo;
            return View();
        }
        public ActionResult Profile()
        {

            ViewBag.Avatar = User.photo;
            ViewBag.FullName = User.Firstname + " " + User.Lastname;
            ViewBag.id = User.id;
            ViewBag.ActiveAvatar = User.photo;
            ViewBag.ActiveFullName = User.Firstname + " " + User.Lastname;
            ViewBag.Activeid = User.id;
            return View();

        }
        public ActionResult ShowShare(string id)
        {
            ViewBag.Avatar = User.photo;
            ViewBag.FullName = User.Firstname + " " + User.Lastname;
            ViewBag.id = User.id;
            ViewBag.Shareid = id;
            ViewBag.ActiveAvatar = User.photo;
            ViewBag.ActiveFullName = User.Firstname + " " + User.Lastname;
            ViewBag.Activeid = User.id;
            return View();
        }
        public ActionResult ShowProfile(string id)
        {
            if (User.id != id)
            {
                FindUserClient Client = new FindUserClient();

                tblUser d = Client.getUser(User.id, id);

                ViewBag.Avatar = d.tblProfiles.First().Avatar; //TODO:Değiştir
                ViewBag.FullName = d.UserName + " " + d.UserSurname;
                ViewBag.id = d._id;
                ViewBag.ActiveAvatar = User.photo;
                ViewBag.ActiveFullName = User.Firstname+ " " + User.Lastname;
                ViewBag.Activeid = User.id;
                if (d.tblFollowings.Count != 0)
                {
                    ViewBag.following = true;
                }
                else
                {
                    ViewBag.following = false;
                }
            }
            else
            {
                ViewBag.Avatar = User.photo;
                ViewBag.FullName = User.Firstname + " " + User.Lastname;
                ViewBag.id = User.id;

            }

            return View("Profile");
        }
        [HttpPost]
        public JsonResult Upload()
        {
            HttpPostedFileBase file = Request.Files[0]; //Uploaded file
            //Use the following properties to get file's name, size and MIMEType
            int fileSize = file.ContentLength;
            string fileName = file.FileName;
            string mimeType = file.ContentType;
            System.IO.Stream fileContent = file.InputStream;
            //To save file, use SaveAs method
            file.SaveAs(Server.MapPath("~/Image/") + fileName);

            return Json(fileName);
        }
    }
}