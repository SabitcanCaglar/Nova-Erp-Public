using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Models.UrunModels
{
    public class Urun:BaseModel
    {
        public string Kodu { get; set; }
        public string Adi { get; set; }
        public int TipId { get; set; }
        public string Birim { get; set; }
        public string RenkKod { get; set; }
        public decimal StandartMaliyet { get; set; }
        public decimal SatisFiyat { get; set; }
        public int UrunIsAkisId { get; set; }
        public int StokAdet { get; set; }
        public int KritikStokSeviyesi { get; set; }
        public int Kapasite { get; set; }
        public string Kategori { get; set; }
        public int AltKategori { get; set; }
        public string UrunResim { get; set; }
        public bool Aktif { get; set; }
    }
}
