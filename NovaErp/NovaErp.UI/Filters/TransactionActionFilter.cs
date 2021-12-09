using Microsoft.AspNetCore.Mvc.Filters;
using NovaErp.Common.GeneralResponse;
using NovaErp.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaErp.UI.Filters
{
    public class TransactionActionFilter : ActionFilterAttribute
    {
        
        public TransactionActionFilter()
        {
          
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {

        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as BaseController;

            if (context.Exception == null)
            {
                controller?.ServiceContext?.Commit();
                GC.SuppressFinalize(this);
            }
            else
            {
                controller?.ServiceContext?.Rollback();
                GC.SuppressFinalize(this);

            }
           
            base.OnActionExecuted(context);

        }


    }
}
