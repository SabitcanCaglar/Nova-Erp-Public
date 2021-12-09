using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NovaErp.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaErp.Filters
{
    public class KullaniciGiris:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as BaseController;
            int? kullaniciId = context.HttpContext.Session.GetInt32("Id");
            
            if (!kullaniciId.HasValue)
            {

                
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {

                    {"action","GirisYap"},
                    {"controller","Login" }
                });
            }
            base.OnActionExecuting(context);
        }

    }
}
