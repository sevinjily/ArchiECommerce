
using System.Globalization;

namespace WebAPI.Middlewares
{
    public class LocalizationMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var lang=context.Request.Headers.AcceptLanguage.FirstOrDefault() ;
            if (lang=="az"||lang=="en-US"||lang=="ru-RU")
            {

                var culture=new CultureInfo(lang);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
           return next(context);
            }
            else
            {
                 var culture=new CultureInfo("az");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
           return next(context);
            }
        }
    }
}
