using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Models
{
    public class Kullanici:BaseModel
    {
        public string KullaniciAdi { get; set; }

        public string Sifre { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int TcKimlikNo { get; set; }
        

    }
}
