
using FluentValidation.AspNetCore;
using Business.DependencyResolver;
using Core.DependencyResolver;

using WebAPI.Middlewares;
using Serilog;

using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

// Konfiqurasiya sistemin? appsettings.json fayl�n� ?lav? edir.
// - "optional: false": Fayl m�tl?q m�vcud olmal�d�r. ?ks halda, t?tbiq x?ta ver?c?k.
// - "reloadOnChange: true": Fayl�n m?zmununda d?yi�iklik olarsa, sistem yenil?nmi� parametrl?ri avtomatik y�kl?y?c?k.
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


// Add services to the container.
builder.Services.AddBusinessService();
builder.Services.AddCoreService();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
FluentValidationMvcExtensions.AddFluentValidation(builder.Services.AddControllersWithViews(), x =>
{
    x.RegisterValidatorsFromAssemblyContaining<Program>();
    x.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("az");
});

builder.Services.AddLogging();
builder.Host.UseSerilog((context, loggerInformation) =>
{
    loggerInformation.ReadFrom.Configuration(context.Configuration);
});


builder.Services.AddTransient<LocalizationMiddleware>();
builder.Services.AddTransient<GlobalHandlingExceptionMiddleware>();

var app = builder.Build();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthentication(); // JWT autentifikasiyas�n� aktivl?�dirir
app.UseAuthorization();

app.UseMiddleware<LocalizationMiddleware>();
app.UseMiddleware<GlobalHandlingExceptionMiddleware>();

app.MapControllers();

app.Run();
