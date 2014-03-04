using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PusherMvc.Web.Controllers
{
    public class ChatController : Controller
    {
        // GET: /Chat/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Auth(string channel_name, string socket_id)
        {
            return Json(new {test = "true"}, JsonRequestBehavior.AllowGet);
        }
    }
}
