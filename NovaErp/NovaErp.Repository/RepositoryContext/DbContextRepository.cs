using NovaErp.Repository.KullaniciRepo;
using NovaErp.Repository.SiparisRepository;

namespace NovaErp.Repository.RepositoryContext
{
    public partial class DbContext
    {
        private MusteriRepository _musteriRepository;
        private UrunRepository _urunRepository;
        private KullaniciRepository _kullaniciRepository;
        private SiparisTaslakRepository _siparisTaslakRepository;
        public MusteriRepository MusteriRepository => _musteriRepository ?? (_musteriRepository = new MusteriRepository(UnitOfWork));
        public UrunRepository UrunRepository => _urunRepository ?? (_urunRepository = new UrunRepository(UnitOfWork));
        public KullaniciRepository KullaniciRepository => _kullaniciRepository ?? (_kullaniciRepository = new KullaniciRepository(UnitOfWork));
        public SiparisTaslakRepository SiparisTaslakRepository => _siparisTaslakRepository ?? (_siparisTaslakRepository = new SiparisTaslakRepository(UnitOfWork));
    }
}
