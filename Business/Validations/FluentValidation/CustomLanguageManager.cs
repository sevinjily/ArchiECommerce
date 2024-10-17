using FluentValidation.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations.FluentValidation
{
    public class CustomLanguageManager:LanguageManager
    {
        public CustomLanguageManager()
        {
            AddTranslation("az", "FirstnameIsRequired", "Ad boş ola bilməz!");
            AddTranslation("ru-RU", "FirstnameIsRequired", "Имя не может быть пустым!");
            AddTranslation("ru-RU", "FirstnameIsRequired", "First name can't be empty!");



        }
    }
}
