
using FormHelper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NovaErp.Models.SiparisModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NovaErp.UI.Models;
using NovaErp.Models.UrunModels;
using NovaErp.Common.CacheService;

namespace NovaErp.UI.Controllers.Siparis
{
    public class YeniSiparisController : BaseController
    {
        private ICacheService _cacheService;
        private readonly ILogger<YeniSiparisController> _logger;
        public TaslakSiparisOlusturModel _taslakSiparisOlusturViewModel { get; set; }


        public YeniSiparisController(ILogger<YeniSiparisController> logger,TaslakSiparisOlusturModel taslakSiparisOlusturViewModel, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
            _taslakSiparisOlusturViewModel = taslakSiparisOlusturViewModel;
            
        }


        public IActionResult YeniSiparis()
        {
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");

            _logger.LogInformation("{AdıSoyadı} tarafından Yeni Sipariş ekranı açıldı ", kullanıcıadsoyad);

            return View();
        }
        public ActionResult MusteriListeGetir()
        {
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKey = "kullanicimusteriliste" + kullancıId;


            try
            {
                if (_cacheService.Any(cacheKey))
                {

                    var kullanıcımusterilistesicache = _cacheService.Get<List<Musteri>>(cacheKey);
                    return Json(new { data = kullanıcımusterilistesicache });
                }
                var kullanıcımusterilistesi = ServiceContext.MusteriService.KullanıcıMusteriListesi(kullancıId);
                _cacheService.Add(cacheKey, kullanıcımusterilistesi.Items);

                return Json(new { data = kullanıcımusterilistesi.Items });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Kullanıcı bazlı müşteri listesi getirilirken {hata} hatası alındı.", ex.Message);
                return Json("Kullanıcı bazlı müşteri listesi getirilirken {hata} hatası alındı.", ex.Message);
            }



        }
        [HttpPost]
        public ActionResult MusteriGüncelle(Musteri musteri)
        {
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKey = "kullanicimusteriliste" + kullancıId;

            var durum = "";

            try
            {
                var guncelmusteri = ServiceContext.MusteriService.MüsteriEkleGüncelle(musteri);
                _logger.LogInformation("{AdıSoyadı} tarafından {id} Id'li müşterisinin bilgileri güncellendi", kullanıcıadsoyad,musteri.Id);
                durum = "basarili";
                _cacheService.Remove(cacheKey);
                _cacheService.Add(cacheKey, ServiceContext.MusteriService.KullanıcıMusteriListesi(kullancıId).Items);
            }
            catch (Exception ex)
            {
                durum = ex.Message;
                _logger.LogInformation("müsteri güncelleme sırasında hata:{hata}", ex.Message);
            }

            return Json(durum);


        }
        [HttpPost, FormValidator]
        public ActionResult MusteriEkle(Musteri musteri)
        {
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKey = "kullanicimusteriliste" + kullancıId;
            if (!ModelState.IsValid)
            {
                var message = ModelState.ToList();
                return FormResult.CreateErrorResult(message[0].Value.Errors.ToString());

            }
            try
            {
                var eklenenmusteri = ServiceContext.MusteriService.MüsteriEkleGüncelle(musteri);
                _logger.LogInformation("{AdıSoyadı} tarafından {müsteri} müşterisi eklendi", kullanıcıadsoyad, musteri.Adi + " " + musteri.Soyadi);
                _cacheService.Remove(cacheKey);
                _cacheService.Add(cacheKey, ServiceContext.MusteriService.KullanıcıMusteriListesi(kullancıId).Items);
                return FormResult.CreateSuccessResult("Yeni Müşteri Başarılı Bir Şekilde Eklenmiştir..");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("müsteri ekleme sırasında hata:{hata}", ex.Message);
                return FormResult.CreateErrorResult(ex.Message);

            }

        }
        [HttpPost]
        public ActionResult MüsteriSil(Musteri musteri)
        {
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKey = "kullanicimusteriliste" + kullancıId;
            var durum = "";
            try
            {

                var silinenmusteri = ServiceContext.MusteriService.MusteriSil(musteri);
                _logger.LogInformation("{AdıSoyadı} tarafından {müsteri} müşterisi silindi.", kullanıcıadsoyad, musteri.Adi + " " + musteri.Soyadi);
                _cacheService.Remove(cacheKey);
                _cacheService.Add(cacheKey, ServiceContext.MusteriService.KullanıcıMusteriListesi(kullancıId).Items);
                durum = "basarili";

            }
            catch (Exception ex)
            {
                _logger.LogInformation("müsteri silme sırasında hata:{hata}", ex.Message);
                durum = ex.Message;

            }
            return Json(durum);
        }

        public IActionResult TaslakSiparisOlustur(Musteri musteri)
        {

            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");

            try
            {
                _taslakSiparisOlusturViewModel.musteri = musteri;
                _taslakSiparisOlusturViewModel.siparisTaslakDetay = ServiceContext.SiparisTaslakService.SiparisTaslakKodOlustur(kullancıId, musteri).Object;
                _logger.LogInformation("{AdıSoyadı} tarafından Taslak Sipariş Oluşturma Ekranı Açıldı", kullanıcıadsoyad);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("{AdıSoyadı} tarafından Taslak Sipariş Oluşturma Ekranı Açılırken {hata} hatası alındı. ", kullanıcıadsoyad,ex.Message);

            }


            return View(_taslakSiparisOlusturViewModel);
        }
        public ActionResult UrunListesi()
        {
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKey = "urunliste";


            try
            {
                if (_cacheService.Any(cacheKey))
                {

                    var urunListesicache = _cacheService.Get<List<Urun>>(cacheKey);
                    return Json(new { data = urunListesicache });
                }
                var urunListesi = ServiceContext.UrunService.UrunListeGetir();
                _cacheService.Add(cacheKey, urunListesi.Items);

                return Json(new { data = urunListesi.Items });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Kullanıcı bazlı taslak sipariş listesi getirilirken {hata} hatası alındı.", ex.Message);
                return Json("Kullanıcı bazlı taslak sipariş listesi getirilirken {hata} hatası alındı.", ex.Message);
            }

        }

        public ActionResult OncekiSiparisler(Musteri musteri)
        {

            var oncekisiparisler = ServiceContext.SiparisTaslakService.MusteriOncekiSiparisListesi(musteri.Id);

            return Json(new { data = oncekisiparisler.Items });

        }

        public ActionResult MusteriTop5Liste(Musteri musteri)
        {


            var top5List = ServiceContext.SiparisTaslakService.MusteriTop5SiparisListesi(musteri.Id).Items;
            
            return Json(top5List);



        }
        [HttpPost]
        public IActionResult UrunEkle(SiparisTaslakDetay siparisTaslakDetay)
        {
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var result ="";
            var cacheKeyKullanici = "kullanicitaslaksiparisliste" + kullancıId;
            var cacheKeyPlanlama = "planlamataslaksiparisliste";
            var siparistaslak = ServiceContext.SiparisTaslakService.SiparisTaslakGetir(siparisTaslakDetay.SiparisTaslakId);
            try
            {

                var stdEkle = ServiceContext.SiparisTaslakService.SiparisTaslakDetayEkleGüncelle(siparisTaslakDetay, kullancıId);
                ServiceContext.SiparisTaslakService.SiparisTaslakFiyatGuncelle(siparisTaslakDetay.SiparisKodu, siparistaslak.Object);
                result = stdEkle.Message;
                CacheGuncelle(cacheKeyKullanici, cacheKeyPlanlama);
                _logger.LogInformation("{AdıSoyadı} tarafından {SiparisKodu} kodlu taslak siparişe {Ürün} kodlu ürün {miktar} {birim} eklendi.", kullanıcıadsoyad,siparisTaslakDetay.SiparisKodu, siparisTaslakDetay.UrunKodu, siparisTaslakDetay.Miktar, siparisTaslakDetay.Birim);

            }
            catch (Exception ex)
            {
                result = ex.Message;
                _logger.LogInformation("{AdıSoyadı} tarafından {SiparisTaslakDetay} kodlu taslak siparişe {Ürün} eklenirken {hata} hatası alındı.", kullanıcıadsoyad, siparisTaslakDetay.SiparisKodu, siparisTaslakDetay.UrunKodu, ex.Message);
            }





            return Json(result);
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


            var result = new { durum=sonuc,siparistaslak=ServiceContext.SiparisTaslakService.SiparisTaslakGetir(siparisTaslakDetay.SiparisTaslakId).Object };


            return Json(sonuc);
        }
        [HttpPost]
        public ActionResult SiparisOnayUrunListesi(SiparisTaslakDetay siparisTaslakDetay)
        {
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));


            var onaysızliste = ServiceContext.SiparisTaslakService.SiparisTaslakDetayListe(siparisTaslakDetay.SiparisKodu,false);

            var toplamadet = onaysızliste.Items.Sum(x => x.Miktar);
            var toplamfiyat = onaysızliste.Items.Sum(x => x.Miktar * x.SatisFiyat);
            var toplamstandartmaliyet = onaysızliste.Items.Sum(x => x.Miktar * x.StandartMaliyet);
            var result = new { liste = onaysızliste.Items, fiyat = toplamfiyat, adet = toplamadet, maliyet = toplamstandartmaliyet };



            return Json(result);


        }
        [HttpPost]
        public IActionResult SiparisOnayUrunSil(SiparisTaslakDetay siparisTaslakDetay)
        {

            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var siparistaslak = ServiceContext.SiparisTaslakService.SiparisTaslakGetir(siparisTaslakDetay.SiparisTaslakId);
            var sonuc = siparistaslak.Message;
            var cacheKeyKullanici = "kullanicitaslaksiparisliste" + kullancıId;
            var cacheKeyPlanlama = "planlamataslaksiparisliste";
            

            try
            {
                var silinenurun = ServiceContext.SiparisTaslakService.SiparisTaslakDetayUrunSil(siparisTaslakDetay.Id);
                
                ServiceContext.SiparisTaslakService.SiparisTaslakFiyatGuncelle(siparisTaslakDetay.SiparisKodu, siparistaslak.Object);
                sonuc = silinenurun.Message;
                CacheGuncelle(cacheKeyKullanici, cacheKeyPlanlama);
                _logger.LogInformation("{AdıSoyadı} tarafından {SiparisTaslakDetay} kodlu taslak siparişte {Ürün} kodlu ürün {miktar} {birim} silindi.", kullanıcıadsoyad, siparisTaslakDetay.SiparisKodu, siparisTaslakDetay.UrunKodu, siparisTaslakDetay.Miktar, siparisTaslakDetay.Birim);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("{AdıSoyadı} tarafından {SiparisTaslakDetay} kodlu taslak siparişte {Ürün} silinirken {hata} hatası alındı.", kullanıcıadsoyad, siparisTaslakDetay.SiparisKodu, siparisTaslakDetay.UrunKodu, ex.Message);

            }



            var result = new { durum =sonuc, siparisTaslak=siparistaslak.Object};




            return Json(result);
        }
        [HttpPost]
        public ActionResult SiparisOnayla(SiparisTaslak siparisTaslak)
        {
            var result = "";
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKeyKullanici = "kullanicitaslaksiparisliste" + kullancıId;
            var cacheKeyPlanlama = "planlamataslaksiparisliste";
            try
            {
                var onaylanansiparistaslak = ServiceContext.SiparisTaslakService.SiparisTaslakOnayla(siparisTaslak, kullancıId);
                var kullanıcıonay = ServiceContext.SiparisTaslakService.SiparisTaslakDetayKullanıcıOnayVer(siparisTaslak.SiparisKodu, siparisTaslak.Id);
                result = onaylanansiparistaslak.Message;

                CacheGuncelle(cacheKeyKullanici, cacheKeyPlanlama);
                _logger.LogInformation("{AdıSoyadı} tarafından {SiparisTaslakDetay} kodlu taslak sipariş onaylandı.", kullanıcıadsoyad, siparisTaslak.SiparisKodu);
            }
            catch (Exception ex)
            {
                result = ex.Message;

            }






            return Json(result);


        }
        [HttpPost]
        public IActionResult PageClosing(SiparisTaslakDetay siparisTaslakDetay)
        {

            var result = "";


            try
            {

                var silinentaslaksiparis = ServiceContext.SiparisTaslakService.SiparisTaslakSil(siparisTaslakDetay.SiparisTaslakId);
                _logger.LogInformation("Sayfa yenilendiği veya kapatıldığı için onay verilmeyen {TaslakSiparisKodu} kodlu taslak sipariş  silindi", siparisTaslakDetay.SiparisKodu);
                result = "basarili";

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Sayfa yenilendiği veya kapatıldığında {TaslakSiparisKodu} kodlu taslak sipariş silinirken {hata} alındı.", siparisTaslakDetay.SiparisKodu, ex.Message);
                result = ex.Message;
            }







            return Json(result);
        }
        [NonAction]
        public void CacheGuncelle(string kullanıcıCacheKey,string planlamaCacheKey)
        {

            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            _cacheService.Remove(kullanıcıCacheKey);
            _cacheService.Add(kullanıcıCacheKey, ServiceContext.SiparisTaslakService.KullaniciTaslakSiparisListesi(kullancıId, false).Items);
            _cacheService.Remove(planlamaCacheKey);
            _cacheService.Add(planlamaCacheKey, ServiceContext.SiparisTaslakService.PlanlamaTaslakSiparisListesi().Items);


        }

    }
}
