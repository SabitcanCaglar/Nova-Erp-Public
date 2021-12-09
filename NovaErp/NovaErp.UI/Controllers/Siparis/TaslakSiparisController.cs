using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using NovaErp.Common.CacheService;
using NovaErp.Models.SiparisModels;
using NovaErp.Models.SiparisModels.TaslakSiparisModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NovaErp.UI.Controllers.Siparis
{

    public class TaslakSiparisController : BaseController
    {
        private ICacheService _cacheService;
        private readonly ILogger<TaslakSiparisController> _logger;
        public TaslakSiparisController(ILogger<TaslakSiparisController> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }
        public IActionResult TaslakSiparis()
        {
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            _logger.LogInformation("{AdıSoyadı} tarafından Yeni Sipariş ekranı açıldı ", kullanıcıadsoyad);
            return View();

        }
        public IActionResult TaslakSiparisListe()
        {
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKey = "kullanicitaslaksiparisliste" + kullancıId;


            try
            {
                if (_cacheService.Any(cacheKey))
                {
                    
                    var taslaksiparisliste = _cacheService.Get<List<SiparisTaslakHelperModel>>(cacheKey);
                    return Json(new { data = taslaksiparisliste });
                }
                var taslaksiparisler = ServiceContext.SiparisTaslakService.KullaniciTaslakSiparisListesi(kullancıId, false);
                _cacheService.Add(cacheKey, taslaksiparisler.Items);

                return Json(new { data = taslaksiparisler.Items });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Kullanıcı bazlı taslak sipariş listesi getirilirken {hata} hatası alındı.", ex.Message);
                return Json("Kullanıcı bazlı taslak sipariş listesi getirilirken {hata} hatası alındı.", ex.Message);
            }

        }
        public ActionResult PersonelSonAy()
        {
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));

            var personelsonaylistesi = ServiceContext.SiparisTaslakService.PersonelSon1AySiparisListesi(kullancıId);

            return Json(personelsonaylistesi.Items);

        }
        [HttpPost]
        public ActionResult SiparisUrunListe(SiparisTaslak siparisTaslak)
        {



            var siparistaslakdetayayrinti = ServiceContext.SiparisTaslakService.SiparisTaslakDetayListe(siparisTaslak.SiparisKodu, true);
            return Json(siparistaslakdetayayrinti.Items);
            


        }
        [HttpPost]
        public ActionResult TaslakSiparisSil(SiparisTaslak siparisTaslak)
        {
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            var cacheKey = "kullanicitaslaksiparisliste" +kullancıId;
            var result = ServiceContext.SiparisTaslakService.SiparisTaslakSil(siparisTaslak.Id);
            _cacheService.Remove(cacheKey);
            var siparistaslakdetayayrinti = ServiceContext.SiparisTaslakService.SiparisTaslakDetayListe(siparisTaslak.SiparisKodu, true);
            _cacheService.Add(cacheKey, siparistaslakdetayayrinti.Items);


            


            return Json(result.Message);


        }
        [HttpPost]
        public ActionResult PlanlamaOnaylıListe()
        {


            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");

            try
            {
                var planlamaonaylıliste = ServiceContext.SiparisTaslakService.KullaniciTaslakSiparisListesi(kullancıId, true);

                return Json(new { data = planlamaonaylıliste.Items });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Kullanıcı bazlı planlama onaylı taslak sipariş listesi getirilirken {hata} hatası alındı.", ex.Message);
                return Json("Kullanıcı bazlı planlama onaylı taslak sipariş listesi getirilirken {hata} hatası alındı.", ex.Message);
            }


        }
        [HttpPost]
        public ActionResult RevizeTalepListe()
        {


            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");

            try
            {
                var revizetalepliste = ServiceContext.SiparisTaslakService.RevizeTalepListesi(kullancıId, false);

                return Json(new { data = revizetalepliste.Items});
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Kullanıcı bazlı revize talep taslak sipariş listesi getirilirken {hata} hatası alındı.", ex.Message);
                return Json("Kullanıcı bazlı revize talep taslak sipariş listesi getirilirken {hata} hatası alındı.", ex.Message);
            }


        }
        [HttpPost]
        public IActionResult UrunGuncelle(SiparisTaslakDetay siparisTaslakDetay)
        {

            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            siparisTaslakDetay.GuncelleyenPersonelId = kullancıId;
            var cacheKeyKullanici = "kullanicitaslaksiparisliste" + kullancıId;
            var cacheKeyPlanlama = "planlamataslaksiparisliste";

            var siparistaslak = ServiceContext.SiparisTaslakService.SiparisTaslakGetir(siparisTaslakDetay.SiparisTaslakId);
            var sonuc = siparistaslak.Message;
            try
            {

                var guncelsiparis = ServiceContext.SiparisTaslakService.SiparisTaslakDetayUrunGuncelle(siparisTaslakDetay);
                ServiceContext.SiparisTaslakService.SiparisTaslakFiyatGuncelle(siparisTaslakDetay.SiparisKodu, siparistaslak.Object);
                sonuc = guncelsiparis.Message;
                CacheGuncelle(cacheKeyKullanici, cacheKeyPlanlama);
                _logger.LogInformation("{AdıSoyadı} tarafından {SiparisTaslakDetay} kodlu taslak siparişte {Ürün} kodlu ürün {miktar} {birim} güncellendi.", kullanıcıadsoyad, siparisTaslakDetay.SiparisKodu, siparisTaslakDetay.UrunKodu, siparisTaslakDetay.Miktar, siparisTaslakDetay.Birim);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("{AdıSoyadı} tarafından {SiparisTaslakDetay} kodlu taslak siparişte {Ürün} güncellenirken {hata} hatası alındı.", kullanıcıadsoyad, siparisTaslakDetay.SiparisKodu, siparisTaslakDetay.UrunKodu, ex.Message);
                sonuc = ex.Message;
            }

            var result = new { durum = sonuc, siparisTaslak = siparistaslak.Object };



            return Json(result);
        }
        [HttpPost]
        public IActionResult KullanıcıRevize(SiparisTaslak siparisTaslak)
        {

            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKeyKullanici = "kullanicitaslaksiparisliste" + kullancıId;
            var cacheKeyPlanlama = "planlamataslaksiparisliste";
            var revizesiparistaslak = ServiceContext.SiparisTaslakService.SiparisTaslakGetir(siparisTaslak.Id).Object;
            revizesiparistaslak.KullaniciRevizeTalepSebebi = siparisTaslak.KullaniciRevizeTalepSebebi;
            revizesiparistaslak.KullaniciRevizeAciklama = siparisTaslak.KullaniciRevizeAciklama;
            var result = ServiceContext.SiparisTaslakService.SiparisTaslakEkleGüncelle(revizesiparistaslak);
            CacheGuncelle(cacheKeyKullanici, cacheKeyPlanlama);
            _logger.LogInformation($"{kullanıcıadsoyad} tarafından {siparisTaslak.SiparisKodu} kodlu taslak sipariş {siparisTaslak.KullaniciRevizeTalepSebebi} sebebi ve '{siparisTaslak.KullaniciRevizeAciklama}' açıklamasıyla revize talebinde bulundu.");
            return Json(result.Message);

        }
        [HttpPost]
        public IActionResult RevizeResponse(SiparisTaslak siparisTaslak)
        {

            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKeyKullanici = "kullanicitaslaksiparisliste" + kullancıId;
            var cacheKeyPlanlama = "planlamataslaksiparisliste";
            var revizesiparistaslak = ServiceContext.SiparisTaslakService.SiparisTaslakGetir(siparisTaslak.Id).Object;
            revizesiparistaslak.PlanlamaRevizeAciklama = null;
            revizesiparistaslak.PlanlamaRevizeTalepSebebi = null;

            var result = ServiceContext.SiparisTaslakService.SiparisTaslakEkleGüncelle(revizesiparistaslak);
            CacheGuncelle(cacheKeyKullanici, cacheKeyPlanlama);
           
            return Json(result.Message);

        }
        [HttpPost]
        public IActionResult TaslakSiparisGuncelle(SiparisTaslak siparisTaslak)
        {

            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKeyKullanici = "kullanicitaslaksiparisliste" + kullancıId;
            var cacheKeyPlanlama = "planlamataslaksiparisliste";
            var revizesiparistaslak = ServiceContext.SiparisTaslakService.SiparisTaslakGetir(siparisTaslak.Id).Object;
            revizesiparistaslak.PoNumber = siparisTaslak.PoNumber;
            revizesiparistaslak.Aciklama = siparisTaslak.Aciklama;
            var result = ServiceContext.SiparisTaslakService.SiparisTaslakEkleGüncelle(revizesiparistaslak);
            CacheGuncelle(cacheKeyKullanici, cacheKeyPlanlama);
            _logger.LogInformation($"{kullanıcıadsoyad} tarafından {siparisTaslak.SiparisKodu} kodlu taslak sipariş Güncellendi.");
            return Json(result.Message);

        }




















        [HttpPost]
        public ActionResult BadgesGuncelle()
        {


            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");

            try
            {
                var revizetalepcount = ServiceContext.SiparisTaslakService.RevizeTalepListesi(kullancıId, false).Items.Count();
                var planlamaonaylıcount= ServiceContext.SiparisTaslakService.KullaniciTaslakSiparisListesi(kullancıId, true).Items.Count();
                var taslaksipariscount= ServiceContext.SiparisTaslakService.KullaniciTaslakSiparisListesi(kullancıId, false).Items.Count();
                return Json(new { revizebagde= revizetalepcount,planlamabadge=planlamaonaylıcount,taslakbadge=taslaksipariscount });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Kullanıcı bazlı revize talep taslak sipariş listesi getirilirken {hata} hatası alındı.", ex.Message);
                return Json("Kullanıcı bazlı revize talep taslak sipariş listesi getirilirken {hata} hatası alındı.", ex.Message);
            }


        }
        [NonAction]
        public void CacheGuncelle(string kullanıcıCacheKey, string planlamaCacheKey)
        {

            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            _cacheService.Remove(kullanıcıCacheKey);
            _cacheService.Add(kullanıcıCacheKey, ServiceContext.SiparisTaslakService.KullaniciTaslakSiparisListesi(kullancıId, false).Items);
            _cacheService.Remove(planlamaCacheKey);
            _cacheService.Add(planlamaCacheKey, ServiceContext.SiparisTaslakService.PlanlamaTaslakSiparisListesi().Items);


        }
    }
}
