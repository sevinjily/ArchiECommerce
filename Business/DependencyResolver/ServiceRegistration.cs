using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<ICategoryService,CategoryManager>();
            services.AddScoped<ICategoryDAL, EFCategoryDAL>();

            services.AddScoped<ISubCategoryService,SubCategoryManager>();
            services.AddScoped<ISubCategoryDAL, EFSubCategoryDAL>();
            services.AddScoped<IAuthService,AuthManager>();

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
