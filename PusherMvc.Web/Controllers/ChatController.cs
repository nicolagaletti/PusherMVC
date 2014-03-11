using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PusherMvc.Web.Contracts;
using System.Security.Cryptography;

namespace PusherMvc.Web.Controllers
{
    public class ChatController : Controller
    {
        private IPusherService _pusherService;

        public ChatController(IPusherService pusherService)
        {
            _pusherService = pusherService;
        }

        // GET: /Chat/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult StartSession(string username)
        {
            if (Session["SessionUserId"] == null)
            {
                Session["SessionUserId"] = username;
            }

            if (Session["userid"] == null)
            {
                using (HashAlgorithm md5 = new MD5CryptoServiceProvider())
                {
                    var str = String.Format("{0}_{1}_{2}", DateTime.UtcNow.Ticks, username, Session.SessionID);

                    byte[] bytes = new byte[str.Length * sizeof(char)];
                    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

                    var result = md5.ComputeHash(bytes);

                    char[] chars = new char[result.Length / sizeof(char)];
                    System.Buffer.BlockCopy(result, 0, chars, 0, result.Length);
                    Session["userid"] = new string(chars);
                }
            }

            return Json(new { success = "true" });
        }

        public ActionResult Auth(string channel_name, string socket_id)
        {
            var userName = "";
            var userid = "";
            object userInfo = null;

            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;
            }
            else
            {
                if (Session["SessionUserId"] != null)
                {
                    userName = (string)Session["SessionUserId"];
                }
                else
                {
                    if (HttpContext.Application["AppUserCount"] == null)
                    {
                        HttpContext.Application["AppUserCount"] = 0;
                    }
                    int newUserCount = ((int)HttpContext.Application["AppUserCount"] + 1);
                    HttpContext.Application["AppUserCount"] = newUserCount;
                    userName = "Guest " + newUserCount;
                    Session["SessionUserId"] = userName;
                }
            }

            if (Session["SessionUserInfo"] == null)
            {
                var objUTC = DateTime.Now.ToUniversalTime();
                var epoch = (objUTC.Ticks - 621355968000000000) / 10000;
                Session["SessionUserInfo"] = new { timestamp = epoch };
            }

            if (Session["userid"] == null)
            {
                using (HashAlgorithm md5 = new MD5CryptoServiceProvider())
                {
                    var str = String.Format("{0}_{1}_{2}", DateTime.UtcNow.Ticks, userName, Session.SessionID);

                    byte[] bytes = new byte[str.Length * sizeof(char)];
                    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

                    var hash = md5.ComputeHash(bytes);

                    char[] chars = new char[hash.Length / sizeof(char)];
                    System.Buffer.BlockCopy(hash, 0, chars, 0, hash.Length);
                    Session["userid"] = new string(chars);
                }
            }

            userInfo = Session["SessionUserInfo"];

            var result = _pusherService.Auth(channel_name, socket_id, userid, userInfo);
            
            //return Json(result, JsonRequestBehavior.AllowGet);

            return new ContentResult {Content = result, ContentType = "application/json"};
        }
    }
}
