
using FluentValidation;
using NovaErp.Models.SiparisModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaErp.UI.Models.Validators
{
    public class MusteriValidator : AbstractValidator<Musteri>
    {

        public MusteriValidator()
        {
            RuleFor(x => x.Adi)
                .NotNull().WithMessage("Müşteri Adı Boş Olamaz..");

            RuleFor(x => x.Soyadi).NotNull().WithMessage("Müşteri Soyadı Boş Olamaz..");
            RuleFor(x => x.Email).NotNull().WithMessage("E-Mail Adresi Boş Olamaz..")
                .EmailAddress();
            RuleFor(x => x.TelefonNo).NotNull().WithMessage("Telefon Bilgisi Boş Olamaz..");
            RuleFor(x => x.Ulke).NotNull().WithMessage("Ülke Bilgisi Boş Olamaz..");
            RuleFor(x => x.Adres).NotNull().WithMessage("Adres Bilgisi Boş Olamaz..");
            RuleFor(x => x.Firma).NotNull().WithMessage("Firma Bilgisi Boş Olamaz..");


        }




    }
}
