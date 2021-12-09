using Nova.Core.UnitOfWork;
using NovaErp.Common;
using NovaErp.Models.UrunModels;
using NovaErp.Models.SiparisModels;
using System;
using System.Collections.Generic;
using System.Text;
using NovaErp.Models.SiparisModels.YeniSiparisModels;
using NovaErp.Models.SiparisModels.TaslakSiparisModels;

namespace NovaErp.Repository.SiparisRepository
{
    public class SiparisTaslakRepository : BaseRepository
    {
        public SiparisTaslakRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {



        }
        public SiparisTaslakDetay SiparisTaslakDetayGetir(SiparisTaslakDetay siparisTaslakDetay)
        {
            var result = DbAction.
                        SQLQuery(@"select * from siparis_taslak_detay
                                       where urunid=@urunId and sipariskodu=@sipariskodu")
                        .SetParameter("urunId", siparisTaslakDetay.UrunId)
                        .SetParameter("sipariskodu", siparisTaslakDetay.SiparisKodu)
                        .SingleResultModel<SiparisTaslakDetay>();
            return result;
        }
        public long SiparisTaslakDetayEkleGüncelle(SiparisTaslakDetay siparisTaslakDetay)
        {
            if (siparisTaslakDetay.Id > 0)
            {
                var result = DbAction.
                      SQLQuery(@"UPDATE public.siparis_taslak_detay SET
                                                            
                                                            siparistaslakid=@siparistaslakid,
                                                            sipariskodu=@sipariskodu,
                                                            sipariskodsıra=@sipariskodsıra,
                                                            urunid=@urunid,
                                                            urunkodu=@urunkodu,
                                                            urunadi=@urunadi,
                                                            miktar=@miktar,
                                                            birim=@birim,
                                                            taslakonay=@taslakonay,
                                                            siparisplanlanantermintarihi=@siparisplanlanantermintarihi,
                                                            eklenmetarihi=@eklenmetarihi,
                                                            guncellenmetarihi=@guncellenmetarihi,
                                                            guncelleyenpersonelid=@guncelleyenpersonelid,
                                                            ekleyenpersonelid=@ekleyenpersonelid
                                                            where id=@id
                                                                     ")
                      .SetParameter("id", siparisTaslakDetay.Id)
                      .SetParameter("siparistaslakid", siparisTaslakDetay.SiparisTaslakId)
                      .SetParameter("sipariskodu", siparisTaslakDetay.SiparisKodu)
                      .SetParameter("sipariskodsıra", siparisTaslakDetay.SiparisKodSıra)
                      .SetParameter("urunid", siparisTaslakDetay.UrunId)
                      .SetParameter("urunkodu", siparisTaslakDetay.UrunKodu)
                      .SetParameter("urunadi", siparisTaslakDetay.UrunAdi)
                      .SetParameter("miktar", siparisTaslakDetay.Miktar)
                      .SetParameter("birim", siparisTaslakDetay.Birim)
                      .SetParameter("taslakonay", siparisTaslakDetay.TaslakOnay)
                      .SetParameter("siparisplanlanantermintarihi", siparisTaslakDetay.SiparisPlanlananTerminTarihi)
                      .SetParameter("eklenmetarihi", siparisTaslakDetay.EklenmeTarihi)
                      .SetParameter("guncellenmetarihi", siparisTaslakDetay.GuncellenmeTarihi)
                      .SetParameter("guncelleyenpersonelid", siparisTaslakDetay.GuncelleyenPersonelId)
                      .SetParameter("ekleyenpersonelid", siparisTaslakDetay.EkleyenPersonelId)
                       .ExecuteUpdateOrInsertOrDelete();
                return result;
            }
            else
            {
                var result = DbAction.
                       SQLQuery(@"INSERT INTO public.siparis_taslak_detay
                                                          ( siparistaslakid,
                                                            sipariskodu,
                                                            sipariskodsıra,
                                                            urunid,
                                                            urunkodu,
                                                            urunadi,
                                                            miktar,
                                                            birim,
                                                            taslakonay,
                                                            siparisplanlanantermintarihi,
                                                            eklenmetarihi,
                                                            guncellenmetarihi,
                                                            guncelleyenpersonelid,
                                                            ekleyenpersonelid)

                                                            VALUES(

                                                            @siparistaslakid,
                                                            @sipariskodu,
                                                            @sipariskodsıra,
                                                            @urunid,
                                                            @urunkodu,
                                                            @urunadi,
                                                            @miktar,
                                                            @birim,
                                                            @taslakonay,
                                                            @siparisplanlanantermintarihi,
                                                            @eklenmetarihi,
                                                            @guncellenmetarihi,
                                                            @guncelleyenpersonelid,
                                                            @ekleyenpersonelid
                                                            );")

                       .SetParameter("siparistaslakid",siparisTaslakDetay.SiparisTaslakId)
                       .SetParameter("sipariskodu", siparisTaslakDetay.SiparisKodu)
                       .SetParameter("sipariskodsıra", siparisTaslakDetay.SiparisKodSıra)
                       .SetParameter("urunid", siparisTaslakDetay.UrunId)
                       .SetParameter("urunkodu", siparisTaslakDetay.UrunKodu)
                       .SetParameter("urunadi", siparisTaslakDetay.UrunAdi)
                       .SetParameter("miktar", siparisTaslakDetay.Miktar)
                       .SetParameter("birim", siparisTaslakDetay.Birim)
                       .SetParameter("taslakonay", siparisTaslakDetay.TaslakOnay)
                       .SetParameter("siparisplanlanantermintarihi", siparisTaslakDetay.SiparisPlanlananTerminTarihi)
                       .SetParameter("eklenmetarihi", siparisTaslakDetay.EklenmeTarihi)
                       .SetParameter("guncellenmetarihi", siparisTaslakDetay.GuncellenmeTarihi)
                       .SetParameter("guncelleyenpersonelid", siparisTaslakDetay.GuncelleyenPersonelId)
                       .SetParameter("ekleyenpersonelid", siparisTaslakDetay.EkleyenPersonelId)
                       .ExecuteUpdateOrInsertOrDelete();

                return result;

            }
        }
        public long SiparisTaslakDetaySil(int Id)
        {
            var result = DbAction.
                        SQLQuery("DELETE FROM public.siparis_taslak_detay WHERE id=@id;")
                        .SetParameter("id",Id)
                        .ExecuteUpdateOrInsertOrDelete();
            return result;
        }
        public long SiparisTaslakDetayKullanıcıOnayver(string siparisKodu,int siparisTaslakId)
        {
            var result = DbAction.
                        SQLQuery(@"UPDATE public.siparis_taslak_detay
	                                                 SET 

	                                                 taslakonay=true
	                                                
	                                                 WHERE siparistaslakid=@siparisTaslakId and sipariskodu=@siparisKodu ")
                        .SetParameter("siparisTaslakId", siparisTaslakId)
                        .SetParameter("siparisKodu", siparisKodu)
                        .ExecuteUpdateOrInsertOrDelete();
            return result;
        }


        public int SiparisTaslakDetayUrunKontrol(SiparisTaslakDetay siparisTaslakDetay)
        {
            var result = DbAction.
                        SQLQuery(@"select count(*) from 
                                       (select * from siparis_taslak_detay
                                       where urunid = @urunId and sipariskodu = @sipariskodu)
                                       as tbl")
                        .SetParameter("urunId", siparisTaslakDetay.UrunId)
                        .SetParameter("sipariskodu", siparisTaslakDetay.SiparisKodu)
                        .SingleResult<int>();
            return result;
        }

        public List<SiparisTaslakDetayHelperModel> SiparisTaslakDetayListe(string siparisKodu,bool taslakonaydurum)
        {
            var result = DbAction.
                        SQLQuery(@"select 
                                         std.id,
                                         std.guncelleyenpersonelid,
                                         std.urunid,
                                         std.sipariskodu,
                                         std.siparistaslakid,
                                         u.adi,
                                         u.kodu,
                                         u.renkkod,
                                         u.kategori,
                                         u.altkategori,
                                         u.stokadet,
                                         u.satisfiyat,
                                         u.standartmaliyet,
                                         std.miktar,
                                         std.birim
                                         
                                         from siparis_taslak_detay std
                                         inner join urun u on u.id=std.urunid
                                         where std.sipariskodu=@sipariskodu and std.taslakonay=@taslakonay 
                                         ")
                        .SetParameter("sipariskodu", siparisKodu)
                        .SetParameter("taslakonay", taslakonaydurum)
                        .ToList<SiparisTaslakDetayHelperModel>();
            return result;
        }







        public SiparisTaslakDetay SiparisTaslakKodOlustur(int ekleyenPersonelId,Musteri musteri)
        {
            var ulkekısaltma = DbAction.
                                       SQLQuery(@"select u.ulkekodu from musteri m
                                                     inner join ulke u on m.ulke = u.ulkeadi
                                                     where m.id =@müsteriid")
                                      .SetParameter("müsteriid", musteri.Id)
                                      .SingleResult<string>();

            SiparisTaslak siparisTaslak = new SiparisTaslak();
            siparisTaslak.GuncellenmeTarihi = DateTime.Now;
            siparisTaslak.EklenmeTarihi = DateTime.Now;
            siparisTaslak.GuncelleyenPersonelId =ekleyenPersonelId;
            siparisTaslak.EkleyenPersonelId = ekleyenPersonelId;
            siparisTaslak.MusteriId = musteri.Id;
            siparisTaslak.Id = Convert.ToInt32(SiparisTaslakEkleGüncelle(siparisTaslak));

            string tarih = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString();
            var siparisTaslakKodu = ("T-" + ulkekısaltma + "-" + ekleyenPersonelId + "-" + tarih + "-" + siparisTaslak.Id);
            siparisTaslak.SiparisKodu = siparisTaslakKodu;
            SiparisTaslakEkleGüncelle(siparisTaslak);

            SiparisTaslakDetay siparisTaslakDetay = new SiparisTaslakDetay();

            siparisTaslakDetay.SiparisKodu = siparisTaslakKodu;
            siparisTaslakDetay.SiparisKodSıra = siparisTaslak.Id;
            siparisTaslakDetay.SiparisTaslakId = siparisTaslak.Id;
            


            return siparisTaslakDetay;
        }
        public long SiparisTaslakEkleGüncelle(SiparisTaslak siparistaslak)
        {
            if (siparistaslak.Id>0)
            {
                var result = DbAction.
                        SQLQuery(@"UPDATE public.siparis_taslak
	                                                 SET 

	                                                 sipariskodu=@sipariskodu,
	                                                 fiyat=@fiyat,
	                                                 miktar=@miktar,
	                                                 standartmaliyet=@standartmaliyet,
	                                                 musteriid=@musteriid,
	                                                 ponumber=@ponumber,
	                                                 aciklama=@aciklama,
	                                                 planlamaonay=@planlamaonay,
	                                                 planlamarevizetalepsebebi=@planlamarevizetalepsebebi,
	                                                 planlamarevizeaciklama=@planlamarevizeaciklama,
	                                                 eklenmetarihi=@eklenmetarihi,
	                                                 guncellenmetarihi=@guncellenmetarihi,
	                                                 guncelleyenpersonelid=@guncelleyenpersonelid,
	                                                 ekleyenpersonelid=@ekleyenpersonelid,
                                                     kullanicirevizetalepsebebi=@kullanicirevizetalepsebebi,
                                                     kullanicirevizeaciklama=@kullanicirevizeaciklama
	                                                 WHERE id=@id;")
                        .SetParameter("id", siparistaslak.Id)
                        .SetParameter("sipariskodu", siparistaslak.SiparisKodu)
                        .SetParameter("fiyat", siparistaslak.Fiyat)
                        .SetParameter("miktar", siparistaslak.Miktar)
                        .SetParameter("standartmaliyet", siparistaslak.StandartMaliyet)
                        .SetParameter("musteriid", siparistaslak.MusteriId)
                        .SetParameter("ponumber", siparistaslak.PoNumber)
                        .SetParameter("aciklama", siparistaslak.Aciklama)
                        .SetParameter("planlamaonay", siparistaslak.PlanlamaOnay)
                        .SetParameter("planlamarevizetalepsebebi", siparistaslak.PlanlamaRevizeTalepSebebi)
                        .SetParameter("planlamarevizeaciklama", siparistaslak.PlanlamaRevizeAciklama)
                        .SetParameter("kullanicirevizetalepsebebi", siparistaslak.KullaniciRevizeTalepSebebi)
                        .SetParameter("kullanicirevizeaciklama", siparistaslak.KullaniciRevizeAciklama)
                        .SetParameter("eklenmetarihi", siparistaslak.EklenmeTarihi)
                        .SetParameter("guncellenmetarihi", siparistaslak.GuncellenmeTarihi)
                        .SetParameter("guncelleyenpersonelid", siparistaslak.GuncelleyenPersonelId)
                        .SetParameter("ekleyenpersonelid", siparistaslak.EkleyenPersonelId)
                        .ExecuteUpdateOrInsertOrDelete();
                return result;
            }
            else
            {
                var result = DbAction.
                       SQLQuery(@"INSERT INTO public.siparis_taslak(
	                                                        
	                                                        sipariskodu, 
	                                                        fiyat,
                                                            miktar,
	                                                        standartmaliyet,
	                                                        musteriid,
	                                                        ponumber,
	                                                        aciklama,
	                                                        planlamaonay,
	                                                        planlamarevizetalepsebebi,
	                                                        planlamarevizeaciklama,
	                                                        eklenmetarihi,
	                                                        guncellenmetarihi,
	                                                        guncelleyenpersonelid,
	                                                        ekleyenpersonelid,
                                                            kullanicirevizetalepsebebi,
                                                            kullanicirevizeaciklama)
	                                                        VALUES (
                                                            
                                                            @sipariskodu, 
	                                                        @fiyat,
                                                            @miktar,
	                                                        @standartmaliyet,
	                                                        @musteriid,
	                                                        @ponumber,
	                                                        @aciklama,
	                                                        @planlamaonay,
	                                                        @planlamarevizetalepsebebi,
	                                                        @planlamarevizeaciklama,
	                                                        @eklenmetarihi,
	                                                        @guncellenmetarihi,
	                                                        @guncelleyenpersonelid,
	                                                        @ekleyenpersonelid,
                                                            @kullanicirevizetalepsebebi,
                                                            @kullanicirevizeaciklama


                                                                        )")

                       .SetParameter("sipariskodu", siparistaslak.SiparisKodu)
                       .SetParameter("fiyat", siparistaslak.Fiyat)
                       .SetParameter("miktar", siparistaslak.Miktar)
                       .SetParameter("standartmaliyet", siparistaslak.StandartMaliyet)
                       .SetParameter("musteriid", siparistaslak.MusteriId)
                       .SetParameter("ponumber", siparistaslak.PoNumber)
                       .SetParameter("aciklama", siparistaslak.Aciklama)
                       .SetParameter("planlamaonay", siparistaslak.PlanlamaOnay)
                       .SetParameter("planlamarevizetalepsebebi", siparistaslak.PlanlamaRevizeTalepSebebi)
                       .SetParameter("planlamarevizeaciklama", siparistaslak.PlanlamaRevizeAciklama)
                       .SetParameter("kullanicirevizetalepsebebi", siparistaslak.KullaniciRevizeTalepSebebi)
                       .SetParameter("kullanicirevizeaciklama", siparistaslak.KullaniciRevizeAciklama)
                       .SetParameter("eklenmetarihi", siparistaslak.EklenmeTarihi)
                       .SetParameter("guncellenmetarihi", siparistaslak.GuncellenmeTarihi)
                       .SetParameter("guncelleyenpersonelid", siparistaslak.GuncelleyenPersonelId)
                       .SetParameter("ekleyenpersonelid", siparistaslak.EkleyenPersonelId)
                       .ExecuteInsertId();
                       
                return result;

            }

            
        }
        public long SiparisTaslakSil(int Id)
        {
            var result = DbAction.
                        SQLQuery("DELETE FROM public.siparis_taslak WHERE id=@id;")
                        .SetParameter("id", Id)
                        .ExecuteUpdateOrInsertOrDelete();
            return result;
        }
        public List<MusteriTop5> MusteriTop5SiparisListesi(int musteriId)
        {
            var result = DbAction.
                        SQLQuery(@"select std.urunkodu,SUM(std.miktar) as toplam
                                     from siparis_taslak_detay std
                                     inner join urun u on u.id=std.urunid
                                     inner join siparis_taslak st on st.id=std.siparistaslakid
                                     inner join musteri m on m.id=st.musteriid
                                     where m.id=@musteriId group by  std.urunkodu 
                                     order by  toplam desc LIMIT 5")
                        .SetParameter("musteriId", musteriId)
                        .ToList<MusteriTop5>();
            return result;
        }
        public List<SiparisTaslakDetayHelperModel> MusteriOncekiSiparisListesi(int musteriId)
        {
            
            var result = DbAction.
                        SQLQuery(@"select 
                                         std.id,
                                         std.guncelleyenpersonelid,
                                         std.urunid,
                                         u.adi,
                                         u.kodu,
                                         u.renkkod,
                                         u.kategori,
                                         u.altkategori,
                                         u.stokadet,
                                         u.satisfiyat,
                                         std.miktar,
                                         std.birim
                                         from siparis_taslak_detay std
                                         inner join urun u on u.id=std.urunid
                                         inner join siparis_taslak st on st.id=std.siparistaslakid
                                         where st.musteriid=@musteriId")
                        .SetParameter("musteriId", musteriId)
                        .ToList<SiparisTaslakDetayHelperModel>();
            return result;
        }
        public List<SiparisTaslakHelperModel> KullaniciTaslakSiparisListesi(int kullaniciId, bool planlamaOnay)
        {

            var result = DbAction.
                        SQLQuery(@"select 
                                    st.id,
                                    st.guncelleyenpersonelid,
                                    st.eklenmetarihi,
                                    st.aciklama,
                                    st.ponumber,
                                    st.sipariskodu,
                                    st.miktar,
                                    st.fiyat,
                                    st.musteriid,
                                    st.kullanicirevizetalepsebebi,
                                    st.kullanicirevizeaciklama,
                                    to_char(st.eklenmetarihi ,'DD/MM/YYYY') as olusturmatarihi,
                                    concat(m.adi,' ',m.soyadi) as musteriadisoyadi
                                    from siparis_taslak st
                                    inner join musteri m on m.id=st.musteriid
                                    inner join musteri_kullanici mk on m.id=mk.musteriid
                                    where mk.kullaniciid=@kullaniciid and st.ekleyenpersonelid=@ekleyenpersonelid and 
                                    st.planlamaonay=@planlamaOnay and st.planlamarevizetalepsebebi is null
                                    
                                    order by st.eklenmetarihi desc")
                        .SetParameter("kullaniciid", kullaniciId)
                        .SetParameter("planlamaOnay", planlamaOnay)
                        .SetParameter("ekleyenpersonelid", kullaniciId)
                        .ToList<SiparisTaslakHelperModel>();
            return result;
        }
        public List<SiparisTaslakHelperModel> RevizeTalepListesi(int kullaniciId, bool planlamaOnay)
        {

            var result = DbAction.
                        SQLQuery(@"select 
                                    st.id,
                                    st.guncelleyenpersonelid,
                                    st.eklenmetarihi,
                                    st.aciklama,
                                    st.ponumber,
                                    st.sipariskodu,
                                    st.miktar,
                                    st.fiyat,
                                    st.musteriid,
                                    st.planlamarevizetalepsebebi,
                                    st.planlamarevizeaciklama,
                                    to_char(st.eklenmetarihi ,'DD/MM/YYYY') as olusturmatarihi,
                                    concat(m.adi,' ',m.soyadi) as musteriadisoyadi
                                    from siparis_taslak st
                                    inner join musteri m on m.id=st.musteriid
                                    inner join musteri_kullanici mk on m.id=mk.musteriid
                                    where mk.kullaniciid=@kullaniciId and st.ekleyenpersonelid=@ekleyenpersonelid and st.planlamaonay=@planlamaOnay and st.planlamarevizetalepsebebi is not null
                                    order by st.eklenmetarihi desc")
                        .SetParameter("kullaniciId", kullaniciId)
                        .SetParameter("ekleyenpersonelid", kullaniciId)
                        .SetParameter("planlamaOnay", planlamaOnay)
                        .ToList<SiparisTaslakHelperModel>();
            return result;
        }
        public List<SiparisTaslakDetay> PersonelSon1AySiparisListesi(int kullanıcıId)
        {

            var result = DbAction.
                        SQLQuery(@"select
                                   std.urunkodu,
                                   sum(std.miktar) as miktar
                                   from siparis_taslak_detay std
                                   where DATE_PART('month', AGE(CURRENT_DATE,std.eklenmetarihi))<=30 and std.ekleyenpersonelid=@kullanıcıId
                                   group by std.urunkodu
                                   order by miktar desc")
                        .SetParameter("kullanıcıId", kullanıcıId)
                        .ToList<SiparisTaslakDetay>();
            return result;
        }
        public SiparisTaslak SiparisTaslakGetir(int Id)
        {
            var result = DbAction.
                        SQLQuery(@"select * from siparis_taslak
                                       where id=@Id")
                        .SetParameter("Id", Id)
                        .SingleResultModel<SiparisTaslak>();
            return result;
        }
        public List<SiparisTaslakHelperModel> PlanlamaTaslakSiparisListesi()
        {

            var result = DbAction.
                        SQLQuery(@"select 
                                    st.id,
                                    st.guncelleyenpersonelid,
                                    st.eklenmetarihi,
                                    st.aciklama,
                                    st.ponumber,
                                    st.sipariskodu,
                                    st.miktar,
                                    st.standartmaliyet,
                                    st.fiyat,
                                    st.musteriid,
                                    to_char(st.eklenmetarihi ,'DD/MM/YYYY') as olusturmatarihi,
                                    concat(m.adi,' ',m.soyadi) as musteriadisoyadi,
                                    st.planlamaonay
                                    from siparis_taslak st
                                    inner join musteri m on m.id=st.musteriid

                                    where st.planlamaonay=false and st.planlamarevizetalepsebebi is  null
                                    order by st.eklenmetarihi desc")

                        .ToList<SiparisTaslakHelperModel>();
            return result;
        }
        

    }
}
