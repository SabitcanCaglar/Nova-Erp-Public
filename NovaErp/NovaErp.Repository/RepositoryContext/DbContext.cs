
using Nova.Core.UnitOfWork;
using NovaErp.Repository.SiparisRepository;

namespace NovaErp.Repository.RepositoryContext
{
    /// <summary>
    /// </summary>
    public partial class DbContext : IDbContextService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private UnitOfWork _unitOfWork;

        public DbContext(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

       

        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = _unitOfWorkFactory.Create());

        public void Commit()
        {
            try
            {
                UnitOfWork.Commit();
            }
            finally
            {
                Reset();
            }
        }

        public void Rollback()
        {
            try
            {
                UnitOfWork.Rollback();
            }
            finally
            {
                Reset();
            }
        }

        void Reset()
        {
            _musteriRepository = null;
            _urunRepository = null;
            _kullaniciRepository = null;

        }
        

    }
}
