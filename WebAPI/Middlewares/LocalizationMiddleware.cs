
using Serilog;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WebAPI.Middlewares
{
    public class LocalizationMiddleware : IMiddleware
    {
        private readonly ILogger<LocalizationMiddleware> _logger;

        public LocalizationMiddleware(ILogger<LocalizationMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var lang=context.Request.Headers.AcceptLanguage.FirstOrDefault() ;
            CultureInfo culture;
            if (lang=="az"||lang=="en-US"||lang=="ru-RU")
            {
                 culture=new CultureInfo(lang);
                _logger.LogInformation($"Setting culture to: {culture.Name}");
                //Log.Error($"Set culture name {culture.Name}");

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
          
            }
            else
            {
                culture = new CultureInfo("az");
                _logger.LogInformation($"Setting culture to: {culture.Name} (default)");
                //Log.Error($"Set culture name {culture.Name}");


            }
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

            await next(context);
        }
    }
}
