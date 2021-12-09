using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Models.SiparisModels.YeniSiparisModels
{
    public class SiparisTaslakDetayHelperModel : BaseModel
    {

        public int UrunId { get; set; }

        public int Miktar { get; set; }
        public string Birim { get; set; }
        public string Kodu { get; set; }
        public string Adi { get; set; }
        public string RenkKod { get; set; }
        public string Kategori { get; set; }
        public int AltKategori { get; set; }
        public int StokAdet { get; set; }
        public decimal SatisFiyat { get; set; }
        public decimal StandartMaliyet { get; set; }
        public string SiparisKodu { get; set; }
        public int SiparisTaslakId { get; set; }

    }
}
