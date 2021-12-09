using Nova.Core.UnitOfWork;
using NovaErp.Common;
using NovaErp.Models.UrunModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Repository.SiparisRepository
{
    public class UrunRepository:BaseRepository
    {
        public UrunRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {


            
        }
        public Urun UrunBilgiGetir(int Id)
        {
            var result = DbAction.
                        SQLQuery("select * from urun where id=@id")
                        .SetParameter("id", Id)
                        .SingleResultModel<Urun>();
            return result;
        }
        public List<Urun> UrunListeGetir()
        {
            var result = DbAction.
                        SQLQuery("select * from urun where aktif=true")
                        .ToList<Urun>();
            return result;
        }
       

    }
}
