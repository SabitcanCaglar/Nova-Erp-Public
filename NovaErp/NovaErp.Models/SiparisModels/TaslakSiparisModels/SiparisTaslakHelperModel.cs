using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Models.SiparisModels.TaslakSiparisModels
{
    public class SiparisTaslakHelperModel:BaseModel
    {
        public string SiparisKodu { get; set; }

        public int Fiyat { get; set; }
        public int Miktar { get; set; }
        public int StandartMaliyet { get; set; }
        public int MusteriId { get; set; }
        public string PoNumber { get; set; }
        public string Aciklama { get; set; }
        public bool PlanlamaOnay { get; set; }
        public string PlanlamaRevizeTalepSebebi { get; set; }
        public string PlanlamaRevizeAciklama { get; set; }

        public string OlusturmaTarihi { get; set; }
        public string MusteriAdiSoyadi { get; set; }

        public string KullaniciRevizeTalepSebebi { get; set; }
        public string KullaniciRevizeAciklama { get; set; }






    }
}
