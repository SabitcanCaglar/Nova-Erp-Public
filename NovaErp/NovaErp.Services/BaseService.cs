using NovaErp.Repository.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Services
{
    public abstract class BaseService
    {
        private IDbContextService _dbContext;
        protected IDbContext RepositoryContext => _dbContext;

        protected BaseService(IDbContextService repositoryContext)
        {
            _dbContext = repositoryContext;
        }

        public void Commit()
        {
            _dbContext.Commit();
        }
        public void Rollback()
        {
            _dbContext.Rollback();
        }
    }
}
