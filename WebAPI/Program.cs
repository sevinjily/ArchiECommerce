
using FluentValidation.AspNetCore;
using Business.DependencyResolver;
using Core.DependencyResolver;

using WebAPI.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

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
//builder.Host.UseSerilog((context, loggerInformation) =>
//{
//    loggerInformation.ReadFrom.Configuration(context.Configuration);
//});


builder.Services.AddTransient<LocalizationMiddleware>();
builder.Services.AddTransient<GlobalHandlingExceptionMiddleware>();

var app = builder.Build();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LocalizationMiddleware>();
app.UseMiddleware<GlobalHandlingExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
