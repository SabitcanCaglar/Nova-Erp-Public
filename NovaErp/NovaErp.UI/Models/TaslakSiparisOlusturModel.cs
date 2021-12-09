
using NovaErp.Models.SiparisModels;
using NovaErp.Models.UrunModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaErp.UI.Models
{
    public class TaslakSiparisOlusturModel
    {

        public Musteri musteri { get; set; }
        public SiparisTaslakDetay siparisTaslakDetay { get; set; }
        public Urun urun { get; set; }


    }
}
