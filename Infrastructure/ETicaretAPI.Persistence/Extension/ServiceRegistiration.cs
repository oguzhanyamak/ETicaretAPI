﻿using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.Basket;
using ETicaretAPI.Application.Repositories.BasketItems;
using ETicaretAPI.Domain.Entities.Identity;
using ETicaretAPI.Infrastructure.Services;
using ETicaretAPI.Persistence.Context;
using ETicaretAPI.Persistence.Repositories;
using ETicaretAPI.Persistence.Repositories.Basket;
using ETicaretAPI.Persistence.Repositories.BasketItems;
using ETicaretAPI.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Extension
{
    public static class ServiceRegistiration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            //services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ETicaretAPIDbContext>();
            services.AddDbContext<ETicaretAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));

            services.AddIdentity<AppUser, AppRole>(options => { 
                options.Password.RequireNonAlphanumeric = false; 
                options.Password.RequiredLength = 3; 
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                }).AddEntityFrameworkStores<ETicaretAPIDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            services.AddScoped<IBasketAsyncWriteRepository, BasketAsyncWriteRepository>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketReadAsyncRepository, BasketAsyncReadRepository>();

            services.AddScoped<IBasketItemAsyncReadRepository, BasketItemAsyncReadRepository>();
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemAsyncWriteRepository, BasketItemAsyncWriteRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

            services.AddScoped<IOrderAsyncReadRepository, OrderAsyncReadRepository>();
            services.AddScoped<IOrderAsyncWriteRepository, OrderAsyncWriteRepository>();
            services.AddScoped<IProductAsyncReadRepository, ProductAsyncReadRepository>();
            services.AddScoped<IProductAsyncWriteRepository, ProductAsyncWriteRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IInternalAuthService, AuthService>();
            services.AddScoped<IExternalAuthService, AuthService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMailService, MailService>();

        }
    }
}
