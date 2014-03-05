using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PusherMvc.Web.Contracts;

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

        public ActionResult Auth(string channel_name, string socket_id)
        {
            var userName = "";
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

            userInfo = Session["SessionUserInfo"];
            
            var result = _pusherService.Auth(channel_name, socket_id, userName, userInfo);
            
            //return Json(result, JsonRequestBehavior.AllowGet);

            return new ContentResult {Content = result, ContentType = "application/json"};
        }
    }
}
