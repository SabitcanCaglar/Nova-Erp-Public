
using FormHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NovaErp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaErp.UI.Controllers
{
    public class LoginController : BaseController
    {
        
        ILogger<LoginController> _logger;
        private IHttpContextAccessor _accessor;

        public LoginController(ILogger<LoginController> logger, IHttpContextAccessor accessor)
        {
            
            _logger = logger;
            _accessor = accessor;


        }

        public ActionResult GirisYap()
        {
            

            return View();
        }




        [HttpPost]
        public ActionResult KullaniciKontrol(Kullanici kullanici)
        {
           
            try
            {
                var kullaniciGR = ServiceContext.KullaniciService.KullaniciAdiveSifreKontrol(kullanici);
                if (kullaniciGR.Success)
                {
                    string ipAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    HttpContext.Session.SetString("IpAdres", ipAddress);
                    HttpContext.Session.SetString("AdSoyad", kullaniciGR.Object.Adi + " " + kullaniciGR.Object.Soyadi);
                    HttpContext.Session.SetInt32("Id", Convert.ToInt32( kullaniciGR.Object.Id));
                    _logger.LogInformation("{KullanıcıAdi} kullanıcısı sisteme giriş yaptı.İP Adresi:{IpAdres}", kullanici.KullaniciAdi, ipAddress.ToString());
                   
                }
               
                
                return Json(kullaniciGR);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                
            }

        }


        public ActionResult CıkısYap()
        {

            HttpContext.Session.Clear();


            return Redirect("GirisYap");
        }


    }
}
