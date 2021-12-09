using Nova.Core.UnitOfWork;
using NovaErp.Common;
using NovaErp.Common.Paged;
using NovaErp.Models.SiparisModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Repository.SiparisRepository
{
    public class MusteriRepository : BaseRepository
    {
        public MusteriRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Musteri MusteriBilgileriGetir(long id)
        {
            var result = DbAction.
                        SQLQuery("select * from musteri where id=@id")
                        .SetParameter("id", id)
                        .SingleResultModel<Musteri>();
            return result;
        }
        public List<Musteri> KullanıcıMusteriListesi(int kullanıcıId)
        {
            var result = DbAction.
                        SQLQuery(@"select m.id,m.adi,m.soyadi,m.email,m.adres,m.ulke,m.telefonno,m.firma,m.guncelleyenpersonelid from musteri m
                        inner join musteri_kullanici mk on m.id = mk.musteriid
                        where    mk.kullaniciid =@kullaniciid order by m.adi")
                        .SetParameter("kullaniciid", kullanıcıId)
                        .ToList<Musteri>();
            return result;
        }
        public long  MusteriEkleGuncelle(Musteri musteri)
        {
            long result;
            if (musteri.Id>0)
            {

                 result= DbAction.
                        SQLQuery(@"UPDATE musteri SET
                                                          adi = @adi,
                                                          soyadi = @soyadi,
                                                          adres = @adres,
                                                          email=@email,
                                                          telefonno = @telefonno,
                                                          ulke=@ulke,
                                                          firma=@firma,
                                                          eklenmetarihi = @eklenmetarihi,
                                                          guncellenmetarihi = @guncellenmetarihi,
                                                          guncelleyenpersonelid = @guncelleyenpersonelid,
                                                          ekleyenpersonelid = @ekleyenpersonelid,
                                                          aktif = @aktif
                                                          WHERE id = @id")
                        .SetParameter("adi",musteri.Adi)
                        .SetParameter("soyadi",musteri.Soyadi)
                        .SetParameter("adres", musteri.Adres)
                        .SetParameter("email", musteri.Email)
                        .SetParameter("telefonno", musteri.TelefonNo)
                        .SetParameter("ulke", musteri.Ulke)
                        .SetParameter("firma", musteri.Firma)
                        .SetParameter("eklenmetarihi", musteri.EklenmeTarihi)
                        .SetParameter("guncellenmetarihi", musteri.GuncellenmeTarihi)
                        .SetParameter("guncelleyenpersonelid", musteri.GuncelleyenPersonelId)
                        .SetParameter("ekleyenpersonelid", musteri.EkleyenPersonelId)
                        .SetParameter("aktif", musteri.Aktif)
                        .SetParameter("id", musteri.Id)
                        .ExecuteUpdateOrInsertOrDelete();






            }
            else
            {

                result = DbAction.
                       SQLQuery(@"INSERT INTO public.musteri
                                                    (
                                                    adi,
                                                    soyadi,
                                                    email,
                                                    telefonno,
                                                    firma,
                                                    ulke,
                                                    adres,
                                                    eklenmetarihi,
                                                    guncellenmetarihi,
                                                    guncelleyenpersonelid,
                                                    ekleyenpersonelid,
                                                    aktif)
                                                    VALUES(
                                                    @adi,
                                                    @soyadi,
                                                    @email,
                                                    @telefonno,
                                                    @firma,
                                                    @ulke,
                                                    @adres,
                                                    @eklenmetarihi,
                                                    @guncellenmetarihi,
                                                    @guncelleyenpersonelid,
                                                    @ekleyenpersonelid,
                                                    @aktif
                                                    );")
                       .SetParameter("adi", musteri.Adi)
                        .SetParameter("soyadi", musteri.Soyadi)
                        .SetParameter("adres", musteri.Adres)
                        .SetParameter("email", musteri.Email)
                        .SetParameter("telefonno", musteri.TelefonNo)
                        .SetParameter("ulke", musteri.Ulke)
                        .SetParameter("firma", musteri.Firma)
                        .SetParameter("eklenmetarihi", musteri.EklenmeTarihi)
                        .SetParameter("guncellenmetarihi", musteri.GuncellenmeTarihi)
                        .SetParameter("guncelleyenpersonelid", musteri.GuncelleyenPersonelId)
                        .SetParameter("ekleyenpersonelid", musteri.EkleyenPersonelId)
                        .SetParameter("aktif", musteri.Aktif)
                       .ExecuteUpdateOrInsertOrDelete();




            }

            
                        
            return result;
        }
        public long MusteriSil(Musteri musteri)
        {
            var result = DbAction.
                        SQLQuery("DELETE FROM public.musteri WHERE id=@id;")
                        .SetParameter("id", musteri.Id)
                        .ExecuteUpdateOrInsertOrDelete();
            return result;
        }

        //public PagedList<Musteri> TumMusteriListesi(int sayfaNo, int kayit)
        //{
        //    var result = DbAction.
        //                SQLQuery("select * from  musteri order by adi")
        //                .ToPagedList<Musteri>(sayfaNo, kayit);
        //    return result;
        //}

    }
}
