using Nova.Core.UnitOfWork;
using NovaErp.Common;
using NovaErp.Common.Paged;
using NovaErp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Repository.KullaniciRepo
{
    public class KullaniciRepository : BaseRepository
    {
        public KullaniciRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Kullanici KullaniciBilgileriGetir(int id)
        {
            var result = DbAction.
                        SQLQuery("select * from kullanici where id=@id")
                        .SetParameter("id", id)
                        .SingleResultModel<Kullanici>();
            return result;
        }
        
        public long  KullaniciEkleGuncelle(Kullanici kullanici)
        {
            long result;
            if (kullanici.Id>0)
            {

                 result= DbAction.
                        SQLQuery(@"UPDATE kullanici SET
                                                          kullaniciadi = @kullaniciadi,
                                                          sifre = @sifre,
                                                          adi = @adi,
                                                          soyadi=@soyadi,
                                                          tckimlikno = @tckimlikno,
                                                          ulke=@ulke,
                                                          firma=@firma,
                                                          eklenmetarihi = @eklenmetarihi,
                                                          guncellenmetarihi = @guncellenmetarihi,
                                                          guncelleyenpersonelid = @guncelleyenpersonelid,
                                                          ekleyenpersonelid = @ekleyenpersonelid,
                                                          aktif = @aktif
                                                          WHERE id = @id")
                        .SetParameter("kullaniciadi", kullanici.KullaniciAdi)
                        .SetParameter("sifre", kullanici.Sifre)
                        .SetParameter("adi", kullanici.Adi)
                        .SetParameter("soyadi", kullanici.Soyadi)
                        .SetParameter("tckimlikno", kullanici.TcKimlikNo)
                        .SetParameter("eklenmetarihi", kullanici.EklenmeTarihi)
                        .SetParameter("guncellenmetarihi", kullanici.GuncellenmeTarihi)
                        .SetParameter("guncelleyenpersonelid", kullanici.GuncelleyenPersonelId)
                        .SetParameter("ekleyenpersonelid", kullanici.EkleyenPersonelId)
                        .SetParameter("id", kullanici.Id)
                        .ExecuteUpdateOrInsertOrDelete();






            }
            else
            {

                result = DbAction.
                       SQLQuery(@"INSERT INTO public.kullanici
                                                   (kullaniciadi,
	                                                sifre,
	                                                adi,
	                                                soyadi,
	                                                tckimlikno,
                                                    eklenmetarihi,
                                                    guncellenmetarihi,
                                                    guncelleyenpersonelid,
                                                    ekleyenpersonelid)
                                                    VALUES(
                                                    @kullaniciadi,
                                                    @sifre,
                                                    @adi,
                                                    @soyadi,
                                                    @tckimlikno,
                                                    @eklenmetarihi,
                                                    @guncellenmetarihi,
                                                    @guncelleyenpersonelid,
                                                    @ekleyenpersonelid,
                                                    );")
                       .SetParameter("kullaniciadi", kullanici.KullaniciAdi)
                        .SetParameter("sifre", kullanici.Sifre)
                        .SetParameter("adi", kullanici.Adi)
                        .SetParameter("soyadi", kullanici.Soyadi)
                        .SetParameter("tckimlikno", kullanici.TcKimlikNo)
                        .SetParameter("eklenmetarihi", kullanici.EklenmeTarihi)
                        .SetParameter("guncellenmetarihi", kullanici.GuncellenmeTarihi)
                        .SetParameter("guncelleyenpersonelid", kullanici.GuncelleyenPersonelId)
                        .SetParameter("ekleyenpersonelid", kullanici.EkleyenPersonelId)
                       .ExecuteUpdateOrInsertOrDelete();




            }

            
                        
            return result;
        }
        public long KullaniciSil(Kullanici kullanici)
        {
            var result = DbAction.
                        SQLQuery("DELETE FROM public.kullanici WHERE id=@id;")
                        .SetParameter("id", kullanici.Id)
                        .ExecuteUpdateOrInsertOrDelete();
            return result;
        }
        public Kullanici KullaniciAdiKontrol(string kullaniciadi)
        {
            var result = DbAction.
                        SQLQuery("select * from kullanici where kullaniciadi=@kullaniciadi")
                        .SetParameter("kullaniciadi", kullaniciadi)
                        .SingleResultModel<Kullanici>();
            return result;
        }
        public Kullanici KullaniciSifreKontrol(string sifre)
        {
            var result = DbAction.
                        SQLQuery("select * from kullanici where sifre=@sifre")
                        .SetParameter("sifre", sifre)
                        .SingleResultModel<Kullanici>();
            return result;
        }

        //public PagedList<kullanici> TumkullaniciListesi(int sayfaNo, int kayit)
        //{
        //    var result = DbAction.
        //                SQLQuery("select * from  kullanici order by adi")
        //                .ToPagedList<kullanici>(sayfaNo, kayit);
        //    return result;
        //}

    }
}
