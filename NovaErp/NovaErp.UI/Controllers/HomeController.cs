using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NovaErp.Models.SiparisModels;
using NovaErp.UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NovaErp.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var result = ServiceContext.MusteriService.MusteriBilgileriGetir(3).Object;
            //var resultliste = ServiceContext.MusteriService.TumMusteriListesi(1, 50).Items;
            //Musteri m1 = new Musteri();
            //m1.Id = 1;
            //m1.Adi = "deneme";
            //m1.Soyadi = "musterisi";
            //var guncelmusteri = ServiceContext.MusteriService.MüsteriEkleGüncelle(m1);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
