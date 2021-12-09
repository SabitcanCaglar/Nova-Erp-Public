using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Models.SiparisModels
{
   public class Musteri:BaseModel
    {

       
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Email { get; set; }
        public string TelefonNo { get; set; }
        public string Firma { get; set; }
        public string Ulke { get; set; }
        public string Adres { get; set; }

        public bool Aktif { get; set; }
    }
}
