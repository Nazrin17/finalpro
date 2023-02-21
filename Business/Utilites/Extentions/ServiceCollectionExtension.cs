using Business.Services.Implementations;
using Business.Services.Intefaces;
using Core.Entities.Concrete;
using DAL;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilites.Extentions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBusinessConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddControllers().AddFluentValidation(opt =>
            //{
            //    opt.ImplicitlyValidateChildProperties = true;
            //    opt.ImplicitlyValidateRootCollectionElements = true;
            //    opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //});
            services.AddDbContext<Hospital2DbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

            }).AddEntityFrameworkStores<Hospital2DbContext>().AddDefaultTokenProviders();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAboutRepository, AboutRepository>();
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped <IPositionRepository, PositionRepository>();
            services.AddScoped <IDoctorRepository, DoctorRepository>();
            services.AddScoped <IResRepository, ResRepository>();
            services.AddScoped<IServiceService, ServiceManager>();
            services.AddScoped<IAboutService, AboutManager>();
            services.AddScoped<IPositionService, PositionManager>();
            services.AddScoped<IDoctorService, DoctorManager>();
            services.AddScoped<IHomeService, HomeManager>();
            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<ISettingService, SettingManager>();
            services.AddScoped<IReservationService, ReservationManager>();
            services.AddScoped<IResHistoryService, ResHistoryManager>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            return services;
        }
    }
}