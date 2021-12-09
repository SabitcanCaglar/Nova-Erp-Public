using FluentValidation;
using NovaErp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPWEBUI.Models.Validators
{
    public class LoginValidator: AbstractValidator<Kullanici>
    {

        public LoginValidator()
        {
            RuleFor(x => x.KullaniciAdi).NotNull().WithMessage("Kullanıcı Adı Boş Olamaz");

            RuleFor(x => x.Sifre).NotNull().WithMessage("Şifre Boş Olamaz");

        }


    }
}
