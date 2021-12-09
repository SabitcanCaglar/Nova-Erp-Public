using NovaErp.Repository.RepositoryContext;
using NovaErp.Services.Services;
using NovaErp.Services.Services.Siparis;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Services.ServiceContext
{
    public class ServiceContext : IServiceContext
    {
        private readonly IDbContextService _dbContext;

        private MusteriService _musteriService;
        private KullaniciService _kullaniciservice;
        private SiparisTaslakService _siparisTaslakService;
        private UrunService _urunService;

        public MusteriService MusteriService => _musteriService ??= new MusteriService(_dbContext);
        public KullaniciService KullaniciService => _kullaniciservice ??= new KullaniciService(_dbContext);
        public SiparisTaslakService SiparisTaslakService => _siparisTaslakService ??= new SiparisTaslakService(_dbContext);
        public UrunService UrunService => _urunService ??= new UrunService(_dbContext);

        public ServiceContext(IDbContextService dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.Commit();
            Reset();
        }

        public void Rollback()
        {
            _dbContext.Rollback();
            Reset();
        }
        void Reset()
        {
            _musteriService = null;
            _kullaniciservice = null;
            _siparisTaslakService = null;
            _urunService = null;


        }
    }
}
