using NovaErp.Common.GeneralResponse;
using NovaErp.Models.SiparisModels;
using NovaErp.Repository.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Services.Services.Siparis
{
    public class MusteriService : BaseService
    {
        public MusteriService(IDbContextService repositoryContext) : base(repositoryContext)
        {
        }

        public GeneralResponse<Musteri> MusteriBilgileriGetir(long musteriId)
        {
            return new GeneralResponse<Musteri>()
            {
                Message = "Herşey yolunda bebeğim :))",
                Success = true,
                Object = RepositoryContext.MusteriRepository.MusteriBilgileriGetir(musteriId)
            };
            
        }
        public GeneralResponseList<Musteri> KullanıcıMusteriListesi(int kullanıcıId)
        {
            return new GeneralResponseList<Musteri>()
            {
                Message = "Herşey yolunda bebeğim :))",
                Success = true,
                Items = RepositoryContext.MusteriRepository.KullanıcıMusteriListesi(kullanıcıId)
            };

        }
        public GeneralResponse<long> MüsteriEkleGüncelle(Musteri musteri)
        {
           var guncellenecekmusteri= MusteriBilgileriGetir(musteri.Id);

            if (musteri.Id > 0)
            {

                    musteri.EklenmeTarihi = guncellenecekmusteri.Object.EklenmeTarihi;
                    musteri.Aktif = true;
                    musteri.GuncellenmeTarihi = DateTime.Now;
                    musteri.EkleyenPersonelId = guncellenecekmusteri.Object.EkleyenPersonelId;
                    

                return new GeneralResponse<long>()
                {
                    Message = "Müşteri Güncelleme İşlemi Başarılı",
                    Success = true,
                    Object = RepositoryContext.MusteriRepository.MusteriEkleGuncelle(musteri)
                };
            }
            else
            {
                musteri.EklenmeTarihi = DateTime.Now;
                musteri.GuncellenmeTarihi = DateTime.Now;
                musteri.Aktif = true;
                
                return new GeneralResponse<long>()
                {
                    Message = "Müşteri Ekleme İşlemi Başarılı",
                    Success = true,
                    Object = RepositoryContext.MusteriRepository.MusteriEkleGuncelle(musteri)
                };

            }

           

        }

        public GeneralResponse<long> MusteriSil(Musteri musteri)
        {
            return new GeneralResponse<long>()
            {
                Message = "Herşey yolunda bebeğim :))",
                Success = true,
                 Object = RepositoryContext.MusteriRepository.MusteriSil(musteri)
            };

        }












        //public GeneralResponseList<Musteri> TumMusteriListesi(int sayfaNo, int kayit)
        //{
        //    return new GeneralResponseList<Musteri>()
        //    {
        //        Message = "Herşey yolunda bebeğim :))",
        //        Success = true,
        //        //Items = RepositoryContext.MusteriRepository.TumMusteriListesi(sayfaNo,kayit).Items   
        //    };

        //}
    }
}
