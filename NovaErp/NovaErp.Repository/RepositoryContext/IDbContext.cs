
using NovaErp.Repository.KullaniciRepo;
using NovaErp.Repository.SiparisRepository;

namespace NovaErp.Repository.RepositoryContext
{
    public interface IDbContext
    {
        void Commit();
        void Rollback();
        MusteriRepository MusteriRepository { get; }
        UrunRepository UrunRepository { get; }
        KullaniciRepository KullaniciRepository { get; }
        SiparisTaslakRepository SiparisTaslakRepository { get; }

    }
}
