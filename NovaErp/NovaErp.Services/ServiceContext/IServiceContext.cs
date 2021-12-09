
using NovaErp.Services.Services;
using NovaErp.Services.Services.Siparis;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Services.ServiceContext
{
    public interface IServiceContext
    {
        void Commit();
        void Rollback();
        MusteriService MusteriService { get; }
        KullaniciService KullaniciService { get; }
        SiparisTaslakService SiparisTaslakService { get; }
        UrunService UrunService { get; }

    }
}
