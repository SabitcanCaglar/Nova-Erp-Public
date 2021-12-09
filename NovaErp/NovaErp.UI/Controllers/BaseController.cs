using Microsoft.AspNetCore.Mvc;
using NovaErp.Services.ServiceContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaErp.UI.Controllers
{
    public class BaseController :Controller
    {
        public IServiceContext ServiceContext;

        public BaseController()
        {
            ServiceContext = ServiceContextFactory.New();
        }
    }
}
