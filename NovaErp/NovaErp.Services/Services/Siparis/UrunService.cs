using NovaErp.Common.GeneralResponse;
using NovaErp.Models.UrunModels;
using NovaErp.Repository.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Services.Services.Siparis
{
    public class UrunService:BaseService
    {
        public UrunService(IDbContextService repositoryContext) : base(repositoryContext)
        {
        }
        public GeneralResponse<Urun> MusteriBilgileriGetir(int urunId)
        {
            return new GeneralResponse<Urun>()
            {
                Message = "Herşey yolunda bebeğim :))",
                Success = true,
                Object = RepositoryContext.UrunRepository.UrunBilgiGetir(urunId)
            };

        }
        public GeneralResponseList<Urun> UrunListeGetir()
        {
            GeneralResponseList<Urun> result = new GeneralResponseList<Urun>();
            try
            {

                result.Success = true;
                result.Items = RepositoryContext.UrunRepository.UrunListeGetir();

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
            

        }
    }
}
