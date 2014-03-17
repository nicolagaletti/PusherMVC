using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PusherMvc.Web.Contracts;
using System.Security.Cryptography;
using PusherMvc.Data.Builders;

namespace PusherMvc.Web.Controllers
{
    public class ChatController : Controller
    {
        private IPusherService _pusherService;
        private const string USER_SESSION_NAME = "sessionuserid";
        private const string USER_SESSION_ID = "userid";
        private const string USER_SESSION_USERINFO = "SessionUserInfo";
        private const string APP_USER_COUNT = "AppUserCount";

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
            var appUserCount = HttpContext.Application[APP_USER_COUNT];
            
            if (appUserCount == null)
            {
                appUserCount = 0;
            }

            int newUserCount = ((int)appUserCount + 1);
            HttpContext.Application[APP_USER_COUNT] = newUserCount;

            appUserCount = newUserCount;

            PusherMvc.Data.Entities.User user = new UserBuilder().WithName(username, (int)appUserCount).WithId(username, Session.SessionID).WithTimestamp();
            
            if (Session[USER_SESSION_NAME] == null)
            {
                Session[USER_SESSION_NAME] = user.Name;
            }

            if (Session[USER_SESSION_ID] == null)
            {
                Session[USER_SESSION_ID] = user.Id;
            }

            if (Session[USER_SESSION_USERINFO] == null)
            {
                Session[USER_SESSION_USERINFO] = new { timestamp = user.Timestamp, username = user.Name };
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
                if (Session[USER_SESSION_NAME] != null)
                {
                    userName = (string)Session[USER_SESSION_NAME];
                }
            }

            if (Session[USER_SESSION_ID] != null)
            {
                userid = (string)Session[USER_SESSION_ID];
            }

            if (Session[USER_SESSION_USERINFO] != null)
            {
                userInfo = Session["SessionUserInfo"];
            }

            var result = _pusherService.Auth(channel_name, socket_id, userid, userInfo);
            
            //return Json(result, JsonRequestBehavior.AllowGet);

            return new ContentResult {Content = result, ContentType = "application/json"};
        }
    }
}
