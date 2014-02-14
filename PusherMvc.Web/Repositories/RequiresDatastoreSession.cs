using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;

namespace PusherMvc.Web.Repositories
{
    public class RequiresDatastoreSession : ActionFilterAttribute, IActionFilter
    {
        public IDocumentSession DocumentSession { get; set; }
        
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                return;
            }

            DocumentSession = DataDocumentStore.Instance.OpenSession();

            this.OnActionExecuting(filterContext);
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                return;
            }

            if (DocumentSession != null && filterContext.Exception == null)
            {
                this.DocumentSession.SaveChanges();
            }

            DocumentSession.Dispose();
            
            this.OnActionExecuted(filterContext);
        }
    }
}