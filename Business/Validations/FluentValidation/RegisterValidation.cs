using Entities.DTOs.AuthDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations.FluentValidation
{
    public class RegisterValidation:AbstractValidator<RegisterDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x=>x.FirstName)
                .NotNull().WithMessage("Ad boş ola bilməz!")
                .NotEmpty().WithMessage(GetTranslation("FirstnameIsRequired"))
                .Must(NonDigit).WithMessage("Rəqəm ola bilməz!").WithName("Ad");
        }
        private bool NonDigit(string value)
        {
            return !value.Any(char.IsDigit);
        }
        private string GetTranslation(string key)
        {
            return ValidatorOptions.Global.LanguageManager.GetString(key,new CultureInfo(Thread.CurrentThread.CurrentCulture.Name));
        }
    }
}
