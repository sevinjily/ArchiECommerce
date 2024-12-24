using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages
{
    public class AuthMessage
    {
        public  const string UserNotFound ;
    }
    private string GetTranslation(string key)
    {

        var culture = CultureInfo.CurrentCulture;


        var translation = ValidatorOptions.Global.LanguageManager.GetString(key, culture);


        return !string.IsNullOrEmpty(translation) ? translation : $"Translation missing for key: {key}";
    }
}
