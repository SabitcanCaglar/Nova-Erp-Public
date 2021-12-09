using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Models.SiparisModels
{
    public class SiparisTaslakDetay:BaseModel
    {
        public int SiparisTaslakId { get; set; }
        public string SiparisKodu { get; set; }
        public int SiparisKodSıra { get; set; }
        public int UrunId { get; set; }
        public string UrunKodu { get; set; }
        public string UrunAdi { get; set; }
        public int Miktar { get; set; }
        public string Birim { get; set; }
        public bool TaslakOnay { get; set; }
        public DateTime SiparisPlanlananTerminTarihi { get; set; }

    }
}
