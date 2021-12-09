using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Models
{
   public  class BaseModel
    {
        public int Id { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime GuncellenmeTarihi { get; set; }
        public int GuncelleyenPersonelId { get; set; }
        public int EkleyenPersonelId { get; set; }
    }
}
