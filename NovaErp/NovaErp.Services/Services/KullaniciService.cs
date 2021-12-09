using NovaErp.Common.GeneralResponse;
using NovaErp.Models;
using NovaErp.Repository.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Services.Services
{
    public class KullaniciService:BaseService
    {
        public KullaniciService(IDbContextService repositoryContext) : base(repositoryContext)
        {



        }


        public GeneralResponse<Kullanici> KullaniciAdiveSifreKontrol(Kullanici kullanici)
        {
            var kullaniciadikontrol=KullaniciAdiKontrol(kullanici.KullaniciAdi);
            var sifrekontrol = KullaniciSifreKontrol(kullanici.Sifre);

            var result = new GeneralResponse<Kullanici>();
            result.Success = false;
            try
            {
                if (kullaniciadikontrol.Success == true && sifrekontrol.Success == false)
                {
                    result.Message = "Şifre Hatalı!";


                }
                else if (kullaniciadikontrol.Success == false && sifrekontrol.Success == true)
                {
                    result.Message = "Kullanıcı Adı Hatalı!";

                }
                else if (kullaniciadikontrol.Success == false && sifrekontrol.Success == false)
                {
                    result.Message = "Kullanıcı Adı ve Şifre Hatalı!";
                }
                else if (kullaniciadikontrol.Object.Adi==sifrekontrol.Object.Adi && kullaniciadikontrol.Object.Sifre==sifrekontrol.Object.Sifre)
                {

                    result.Success = true;
                    result.Object = KullaniciAdiKontrol(kullanici.KullaniciAdi).Object;
                    result.Message = $"Hoşgeldiniz.. {result.Object.Adi + " " + result.Object.Soyadi} ANASAYFA'ya Yönlendiriliyorsunuz.";
                }
                else
                {
                    result.Message = "Kullanıcı Adı veya Şifre Hatalı";
                }
                


            }
            catch (Exception ex)
            {

                result.Message = ex.Message;
            }

            return result;



        }





        public GeneralResponse<Kullanici> KullaniciAdiKontrol(string kullaniciadi)
        {
            var result = new GeneralResponse<Kullanici>();
           
            try
            {
                result.Object = RepositoryContext.KullaniciRepository.KullaniciAdiKontrol(kullaniciadi);
                result.Message = "Kullanıcı Adı İle Eşleşen Kayıt Mevcuttur";
                result.Success = true;
                return result;
               
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
            

        }
        public GeneralResponse<Kullanici> KullaniciSifreKontrol(string sifre)
        {
            var result = new GeneralResponse<Kullanici>();
            try
            {
                result.Object = RepositoryContext.KullaniciRepository.KullaniciSifreKontrol(sifre);
                result.Message = "Şifre İle Eşleşen Kayıt Mevcuttur";
                result.Success = true;
                return result;

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }


        }
    }
}
