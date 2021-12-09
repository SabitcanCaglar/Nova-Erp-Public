using NovaErp.Repository.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Services.ServiceContext
{
    public sealed class ServiceContextFactory
    {
        public static ServiceContext New()
        {
            return new ServiceContext(RepositoryContext.New());
        }
    }
}
