using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Interfaces;
using POS.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Extensions
{
    public static class InjectionExtencions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(c => !c.IsDynamic));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICategoryApplication,CategoryApplication>();
            return services;
        }
    }
}
