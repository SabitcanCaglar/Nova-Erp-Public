using NovaErp.Common.GeneralResponse;
using NovaErp.Models.SiparisModels;
using NovaErp.Models.SiparisModels.TaslakSiparisModels;
using NovaErp.Models.SiparisModels.YeniSiparisModels;
using NovaErp.Repository.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NovaErp.Services.Services.Siparis
{
    public class SiparisTaslakService:BaseService
    {
        
        public SiparisTaslakService(IDbContextService repositoryContext) : base(repositoryContext)
        {
        }
        public GeneralResponse<SiparisTaslakDetay> SiparisTaslakKodOlustur(int ekleyenPersonelId ,Musteri musteri)
        {
            return new GeneralResponse<SiparisTaslakDetay>()
            {
                Object = RepositoryContext.SiparisTaslakRepository.SiparisTaslakKodOlustur(ekleyenPersonelId,musteri)
            };

        }
        public GeneralResponse<long> SiparisTaslakEkleGüncelle(SiparisTaslak siparisTaslak)
        {
            GeneralResponse<long> result = new GeneralResponse<long>();
            try
            {
                result.Object = RepositoryContext.SiparisTaslakRepository.SiparisTaslakEkleGüncelle(siparisTaslak);
                result.Message = "basarili";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

            }
            return result;

        }
        public GeneralResponseList<MusteriTop5> MusteriTop5SiparisListesi(int musteriId)
        {
            return new GeneralResponseList<MusteriTop5>()
            {
                Items = RepositoryContext.SiparisTaslakRepository.MusteriTop5SiparisListesi(musteriId)
            };

        }
        public GeneralResponseList<SiparisTaslakDetayHelperModel> MusteriOncekiSiparisListesi(int musteriId)
        {
            return new GeneralResponseList<SiparisTaslakDetayHelperModel>()
            {
                Items = RepositoryContext.SiparisTaslakRepository.MusteriOncekiSiparisListesi(musteriId)
            };

        }
        public GeneralResponse<long> SiparisTaslakDetayEkleGüncelle(SiparisTaslakDetay siparisTaslakDetay,int kullanıcıId)
        {
            GeneralResponse<long> result = new GeneralResponse<long>();

            siparisTaslakDetay.EklenmeTarihi = DateTime.Now;
            siparisTaslakDetay.GuncellenmeTarihi = DateTime.Now;
            siparisTaslakDetay.SiparisPlanlananTerminTarihi = DateTime.Now;
            siparisTaslakDetay.EkleyenPersonelId = kullanıcıId;

           var urunkontrol=RepositoryContext.SiparisTaslakRepository.SiparisTaslakDetayUrunKontrol(siparisTaslakDetay);
            if (urunkontrol>0)
            {
                result.Message= "urunguncelle";
            }
            else
            {
                try
                {
                    result.Object = RepositoryContext.SiparisTaslakRepository.SiparisTaslakDetayEkleGüncelle(siparisTaslakDetay);
                    result.Success = true;
                    result.Message = "basarili";
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;

                }

            }


           

            return result;


        }
        public GeneralResponse<SiparisTaslakDetay> SiparisTaslakDetayUrunGuncelle(SiparisTaslakDetay siparisTaslakDetay)
        {
            GeneralResponse<SiparisTaslakDetay> result = new GeneralResponse<SiparisTaslakDetay>();

          
           try
           {
                result.Object = RepositoryContext.SiparisTaslakRepository.SiparisTaslakDetayGetir(siparisTaslakDetay);

                result.Object.Miktar = siparisTaslakDetay.Miktar;
                result.Object.Birim = siparisTaslakDetay.Birim;
                result.Object.GuncellenmeTarihi = DateTime.Now;
                RepositoryContext.SiparisTaslakRepository.SiparisTaslakDetayEkleGüncelle(result.Object);
                
                result.Success = true;
               result.Message = "basarili";
           }
           catch (Exception ex)
           {
               result.Message = ex.Message;

           }

            




            return result;


        }
        public GeneralResponse<long> SiparisTaslakDetayKullanıcıOnayVer(string siparisKodu, int siparisTaslakId)
        {



            return new GeneralResponse<long>()
            {
                Object = RepositoryContext.SiparisTaslakRepository.SiparisTaslakDetayKullanıcıOnayver(siparisKodu, siparisTaslakId),
                Message = "basarili"
            };
        }
        public GeneralResponse<long> SiparisTaslakDetayUrunSil(int Id)
        {
            GeneralResponse<long> result = new GeneralResponse<long>();


            try
            {
                result.Object=RepositoryContext.SiparisTaslakRepository.SiparisTaslakDetaySil(Id);
                result.Success = true;
                result.Message = "basarili";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

            }






            return result;


        }

        public GeneralResponse<long> SiparisTaslakOnayla (SiparisTaslak siparisTaslak , int kullanıcıId)
        {
            GeneralResponse<long> result = new GeneralResponse<long>();

            siparisTaslak.EkleyenPersonelId = kullanıcıId;
            siparisTaslak.GuncelleyenPersonelId = kullanıcıId;
            siparisTaslak.GuncellenmeTarihi = DateTime.Now;
            siparisTaslak.EklenmeTarihi = DateTime.Now;

            try
            {
                result.Object = RepositoryContext.SiparisTaslakRepository.SiparisTaslakEkleGüncelle(siparisTaslak);
                result.Success = true;
                result.Message = "basarili";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

            }






            return result;


        }
        public GeneralResponseList<SiparisTaslakDetayHelperModel> SiparisTaslakDetayListe(string siparisKodu, bool taslakonaydurum)
        {
            GeneralResponseList<SiparisTaslakDetayHelperModel> onaysızliste = new GeneralResponseList<SiparisTaslakDetayHelperModel>();
            onaysızliste.Items = RepositoryContext.SiparisTaslakRepository.SiparisTaslakDetayListe(siparisKodu, taslakonaydurum);
            return onaysızliste;
        }
        public GeneralResponse<long> SiparisTaslakSil( int siparisTaslakId)
        {
            GeneralResponse<long> result = new GeneralResponse<long>();
            try
            {
                result.Object = RepositoryContext.SiparisTaslakRepository.SiparisTaslakSil(siparisTaslakId);
                result.Message = "basarili";

            }
            catch (Exception ex)
            {

                result.Message = ex.Message;
            }
            return result;
        }
        public GeneralResponse<SiparisTaslak> SiparisTaslakGetir(int Id)
        {

            return new GeneralResponse<SiparisTaslak>()
            {
                Object = RepositoryContext.SiparisTaslakRepository.SiparisTaslakGetir(Id),
                Message = "basarili"
            };
        }



        public GeneralResponseList<SiparisTaslakHelperModel> KullaniciTaslakSiparisListesi(int kullanıcıId,bool planlamaOnay)
        {
            return new GeneralResponseList<SiparisTaslakHelperModel>()
            {
                Items = RepositoryContext.SiparisTaslakRepository.KullaniciTaslakSiparisListesi(kullanıcıId, planlamaOnay)
            };

        }

        public GeneralResponseList<SiparisTaslakDetay> PersonelSon1AySiparisListesi(int kullanıcıId)
        {
            return new GeneralResponseList<SiparisTaslakDetay>()
            {
                Items = RepositoryContext.SiparisTaslakRepository.PersonelSon1AySiparisListesi(kullanıcıId)
            };

        }

        public GeneralResponse<string> SiparisTaslakFiyatGuncelle(string siparisKodu,SiparisTaslak siparisTaslak)
        {
            GeneralResponse<string> result = new GeneralResponse<string>();


            try
            {
                var siparistdlist = RepositoryContext.SiparisTaslakRepository.SiparisTaslakDetayListe(siparisKodu, true);
                siparisTaslak.Miktar = siparistdlist.Sum(x => x.Miktar);
                siparisTaslak.Fiyat = siparistdlist.Sum(x => x.SatisFiyat) * siparisTaslak.Miktar;
                siparisTaslak.StandartMaliyet = siparistdlist.Sum(x => x.StandartMaliyet)*siparisTaslak.Miktar;
                siparisTaslak.GuncellenmeTarihi = DateTime.Now;

               RepositoryContext.SiparisTaslakRepository.SiparisTaslakEkleGüncelle(siparisTaslak);
               
            }
            catch (Exception ex)
            {
                 result.Message = ex.Message;

            }
            return result;
            
        }
        public GeneralResponseList<SiparisTaslakHelperModel> PlanlamaTaslakSiparisListesi()
        {
            return new GeneralResponseList<SiparisTaslakHelperModel>()
            {
                Items = RepositoryContext.SiparisTaslakRepository.PlanlamaTaslakSiparisListesi()
            };

        }
        public GeneralResponseList<SiparisTaslakHelperModel> RevizeTalepListesi(int kullanıcıId, bool planlamaOnay)
        {
            return new GeneralResponseList<SiparisTaslakHelperModel>()
            {
                Items = RepositoryContext.SiparisTaslakRepository.RevizeTalepListesi(kullanıcıId, planlamaOnay)
            };

        }




    }
    
}

