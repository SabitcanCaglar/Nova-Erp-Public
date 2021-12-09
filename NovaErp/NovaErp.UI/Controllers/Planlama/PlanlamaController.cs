using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NovaErp.Common.CacheService;
using NovaErp.Models.SiparisModels;
using NovaErp.Models.SiparisModels.TaslakSiparisModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaErp.UI.Controllers.Planlama
{
    public class PlanlamaController : BaseController
    {
        private ICacheService _cacheService;
        private readonly ILogger<PlanlamaController> _logger;
        public PlanlamaController(ILogger<PlanlamaController> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }
        public IActionResult PlanlamaTaslakSiparisler()
        {
            return View();
        }
        public IActionResult TaslakSiparisListesi()
        {
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKey = "planlamataslaksiparisliste";


            try
            {
                if (_cacheService.Any(cacheKey))
                {

                    var planlamataslaksiparislistecache = _cacheService.Get<List<SiparisTaslakHelperModel>>(cacheKey);
                    return Json(new { data = planlamataslaksiparislistecache });
                }
                var planlamataslaksiparisliste = ServiceContext.SiparisTaslakService.PlanlamaTaslakSiparisListesi();
                _cacheService.Add(cacheKey, planlamataslaksiparisliste.Items);

                return Json(new { data = planlamataslaksiparisliste.Items });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Kullanıcı bazlı taslak sipariş listesi getirilirken {hata} hatası alındı.", ex.Message);
                return Json("Kullanıcı bazlı taslak sipariş listesi getirilirken {hata} hatası alındı.", ex.Message);
            }
            

        }


        [HttpPost]
        public IActionResult PlanlamaOnay(SiparisTaslak siparisTaslak)
        {

            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKeyKullanici = "kullanicitaslaksiparisliste" + kullancıId;
            var cacheKeyPlanlama = "planlamataslaksiparisliste";
            siparisTaslak = ServiceContext.SiparisTaslakService.SiparisTaslakGetir(siparisTaslak.Id).Object;
            siparisTaslak.PlanlamaOnay = true;
            var result = ServiceContext.SiparisTaslakService.SiparisTaslakEkleGüncelle(siparisTaslak);
            _cacheService.Remove("planlamataslaksiparisliste");
            _cacheService.Add("planlamataslaksiparisliste", ServiceContext.SiparisTaslakService.PlanlamaTaslakSiparisListesi().Items);
            CacheGuncelle(cacheKeyKullanici, cacheKeyPlanlama);
            return Json(result.Message);
        }
        [HttpPost]
        public IActionResult PlanlamaRevize(SiparisTaslak siparisTaslak)
        {
            
            int kullancıId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            string kullanıcıadsoyad = HttpContext.Session.GetString("AdSoyad");
            var cacheKeyKullanici = "kullanicitaslaksiparisliste" + kullancıId;
            var cacheKeyPlanlama = "planlamataslaksiparisliste";
            var revizesiparistaslak = ServiceContext.SiparisTaslakService.SiparisTaslakGetir(siparisTaslak.Id).Object;
            revizesiparistaslak.PlanlamaRevizeTalepSebebi = siparisTaslak.PlanlamaRevizeTalepSebebi;
            revizesiparistaslak.PlanlamaRevizeAciklama = siparisTaslak.PlanlamaRevizeAciklama;
            var result = ServiceContext.SiparisTaslakService.SiparisTaslakEkleGüncelle(revizesiparistaslak);
            CacheGuncelle(cacheKeyKullanici, cacheKeyPlanlama);
            _logger.LogInformation($"{kullanıcıadsoyad} tarafından {siparisTaslak.SiparisKodu} kodlu taslak sipariş {siparisTaslak.PlanlamaRevizeTalepSebebi} sebebi ve '{siparisTaslak.PlanlamaRevizeAciklama}' açıklamasıyla revize talebinde bulundu.");

            return Json(result.Message);
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
