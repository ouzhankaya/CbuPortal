using CbuPortal.Models;
using CbuPortal.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CbuPortal.LoginService;
using CbuPortal.ShareEventsService;
using CbuPortal.FollowService;
using CbuPortal.FindUser;
using CbuPortal.NotificationEvents;
using CbuPortal.AccountService;
using Newtonsoft.Json;
using System.Web.Security;
using MongoDB.Bson;

namespace CbuPortal.Controllers
{
    public class DataController : BaseController
    {
        [HttpPost]
        public JsonResult Paylasilanlar()
        {
            try
            {
                ShareEventsClient Client = new ShareEventsClient();  //TODO: Beğen sistemi ayarlanacak
                ObjectId id = new ObjectId(User.id);
                var shares = Client.getFriendsShares(id);
                return Json(shares);
            }
            catch
            {
                return null;
            }

        }


        [HttpPost]
        public JsonResult Paylastiklarim(string sno)
        {
            try
            {
                ShareEventsClient Client = new ShareEventsClient();
                ObjectId id = new ObjectId(sno);
                var shares = Client.getUserShares(id);
                return Json(shares);
            }
            catch
            {
                return null;
            }

        }
        [HttpPost]
        public JsonResult Paylas(string Share)
        {
            try
            {
                tblShare ShareRequest = new tblShare();
                ShareEventsClient Client = new ShareEventsClient();
                ObjectId id = new ObjectId(User.id);
                ShareRequest.SharingDate = DateTime.Now;
                ShareRequest.SharingText = Share;
                ShareRequest.UserID = id;
                string a = Client.setShare(ShareRequest).ToString();
                ShareRequest._id = new ObjectId(a);
                return Json(new { nameSurname = User.Firstname + " " + User.Lastname, text = ShareRequest.SharingText, dt = ShareRequest.SharingDate, resim = User.photo, id = ShareRequest._id.ToString(), userId = User.id });
            }
            catch
            {
                return null;
            }
        }


        [HttpPost]
        public JsonResult Followers(string sno)
        {
            try
            {
                FollowEventsClient Client = new FollowEventsClient();
                ObjectId id = new ObjectId(sno);
                var follower = Client.getFollowers(id);
                return Json(follower);
            }
            catch
            {
                return null;
            }
        }
        [HttpPost]
        public JsonResult Following(string sno)
        {
            try
            {
                FollowEventsClient Client = new FollowEventsClient();
                ObjectId id = new ObjectId(sno);
                var follower = Client.getFollowing(id);
                return Json(follower);
            }
            catch
            {
                return null;
            }
        }
        [HttpPost]
        public bool Begen(string id, bool status)
        {
            try
            {
                ShareEventsClient Client = new ShareEventsClient();
                ObjectId ShareId = new ObjectId(id);
                tblSharesLike sending = new tblSharesLike();
                sending.SharesLikeDate = DateTime.Now;
                sending.SharingID = ShareId;
                sending.SharesLikeUserID = new ObjectId(User.id);
                bool result = Client.shareLike(sending, status);
                NotificationEventsClient NotClient = new NotificationEventsClient();
                var x = NotClient.NotificationsGetShare(ShareId);
                if (User.id != x.UserID.ToString())
                    NotClient.NotificationAdd(sending.SharesLikeUserID, sending.SharingID, 1);
                return result;
            }
            catch
            {
                return false;
            }
        }


        [HttpPost]
        public bool YorumYap(string id, string content)
        {
            try
            {
                ShareEventsClient Client = new ShareEventsClient();
                ObjectId ShareId = new ObjectId(id);
                tblSharesComment sending = new tblSharesComment();
                NotificationEventsClient NotClient = new NotificationEventsClient();
                var x = NotClient.NotificationsGetShare(ShareId);
                sending.SharesCommentDate = DateTime.Now;
                sending.SharesCommentText = content;
                sending.SharesCommentUsersID = new ObjectId(User.id);
                sending.SharingID = new ObjectId(id);
                if (User.id != x.UserID.ToString())
                    NotClient.NotificationAdd(sending.SharesCommentUsersID, sending.SharingID, 2);
                bool result = Client.shareComment(sending);
                return result;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public JsonResult Arama(string d)
        {
            try
            {
                FindUserClient Client = new FindUserClient();
                tblFoundUsers[] Users = Client.findUser(d);
                var Data = Users.Select(c => new { id = c.userID.ToString(), label = c.userNameSurname, img = c.avatar });
                return Json(new { Users = Data });
            }
            catch
            {
                return null;
            }
        }
        [HttpPost]
        public bool Takip(string id, bool FollowStatus)
        {
            try
            {
                bool durum;

                tblFollowing d = new tblFollowing();
                NotificationEventsClient NotClient = new NotificationEventsClient();
                d.FollowDate = DateTime.Now;
                d.UserID = new ObjectId(User.id);
                d.FollowingUserID = new ObjectId(id);
                FollowEventsClient Client = new FollowEventsClient();
                durum = Client.setFollow(d, FollowStatus);
                if (FollowStatus)
                    NotClient.NotificationAdd(d.FollowingUserID, d.UserID, 3);
                return durum;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public JsonResult getShare(string id)
        {
            try
            {
                ObjectId d = new ObjectId(id);
                NotificationEventsClient Client = new NotificationEventsClient();
                var share = Client.NotificationsGetShare(d);
                return Json(share);
            }
            catch
            {
                return null;
            }
        }
        [HttpPost]
        public JsonResult BildirimeriAl(string id)
        {
            ObjectId Sno = new ObjectId(id);
            NotificationEventsClient Client = new NotificationEventsClient();
            tblNotificationsInformation[] d = Client.NotificationsFalseGet(Sno);

            return Json(d);
        }

        [HttpGet]
        public bool SetSeemTrue()
        {
            try
            {

                NotificationEventsClient Client = new NotificationEventsClient();
                Client.NotificationsSeem(new ObjectId(User.id));
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost]
        public bool ChangeAvatar(string Avatar)
        {
            try
            {

                NotificationEventsClient Client = new NotificationEventsClient();
                Client.updateAvatar(new ObjectId(User.id), Avatar);
                var serializeModel = new CustomPrincipalSerializeModel
                {
                    id = User.id,
                    photo = Avatar,
                    Firstname = User.Firstname,//Servisten Gelecek
                    Lastname = User.Lastname,//Servisten Gelecek 
                    Username = User.Username,
                    Password = User.Password,


                };

                string userData = JsonConvert.SerializeObject(serializeModel);
                #region Kullanıcı 180dk sonra loginden düşmesi için tutulan cookie

                var authTicket = new FormsAuthenticationTicket(
                         1,
                         User.Username,
                         DateTime.Now,
                         DateTime.Now.AddMinutes(120),
                         true,
                         userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.Expires = DateTime.MaxValue;
                Response.Cookies.Add(cookie);
                #endregion
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public bool YorumSil(string id)
        {
            ShareEventsClient client = new ShareEventsClient();
            client.userCommentDelete(new ObjectId(id));
            return true;
        }
        public bool PaylasimSil(string id)
        {
            ShareEventsClient client = new ShareEventsClient();
            client.userDeleteShare(new ObjectId(id));
            return true;
        }
    }
}