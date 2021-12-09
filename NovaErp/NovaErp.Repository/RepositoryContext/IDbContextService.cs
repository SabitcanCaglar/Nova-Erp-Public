using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Repository.RepositoryContext
{
    public interface IDbContextService : IDbContext
    {
        void Commit();
        void Rollback();
    }
}
