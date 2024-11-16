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
                .NotNull().WithMessage(GetTranslation("FirstnameIsRequired"))
                .NotEmpty().WithMessage(GetTranslation("FirstnameIsRequired"))
                .Must(NonDigit).WithMessage("Rəqəm ola bilməz!").WithName("Ad");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage(GetTranslation("LastnameIsRequired"))
                .NotEmpty().WithMessage(GetTranslation("LastnameIsRequired"))
                .Must(NonDigit).WithMessage("Rəqəm ola bilməz!")
                .WithName("Soyad");

            RuleFor(x => x.UserName)
                .NotNull().WithMessage(GetTranslation("UsernameIsRequired"))
                .NotEmpty().WithMessage(GetTranslation("UsernameIsRequired"))
                .Matches("^[a-zA-Z0-9]*$").WithMessage(GetTranslation("UsernameInvalid"))
                .WithName("İstifadəçi adı");

            RuleFor(x => x.Email)
                .NotNull().WithMessage(GetTranslation("EmailIsRequired"))
                .NotEmpty().WithMessage(GetTranslation("EmailIsRequired"))
                .EmailAddress().WithMessage(GetTranslation("InvalidEmailFormat"))
                .WithName("Email");

            RuleFor(x => x.Password)
                .NotNull().WithMessage(GetTranslation("PasswordIsRequired"))
                .NotEmpty().WithMessage(GetTranslation("PasswordIsRequired"))
                .MinimumLength(8).WithMessage(GetTranslation("PasswordMinLength"))
                .Matches("[A-Z]").WithMessage(GetTranslation("PasswordMustContainUppercase"))
                .Matches("[a-z]").WithMessage(GetTranslation("PasswordMustContainLowercase"))
                .Matches("[0-9]").WithMessage(GetTranslation("PasswordMustContainDigit"))
                .Matches("[^a-zA-Z0-9]").WithMessage(GetTranslation("PasswordMustContainSpecialCharacter"))
                .WithName("Şifrə");

            RuleFor(x => x.ConfirmPassword)
                 .NotNull().WithMessage(GetTranslation("ConfirmPasswordIsRequired"))
                 .NotEmpty().WithMessage(GetTranslation("ConfirmPasswordIsRequired"))
                 .Equal(x => x.Password).WithMessage(GetTranslation("PasswordsDoNotMatch"))
                 .WithName("Təsdiq Şifrəsi");


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
