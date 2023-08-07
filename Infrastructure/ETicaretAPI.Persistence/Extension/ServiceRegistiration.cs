﻿using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persistence.Context;
using ETicaretAPI.Persistence.Repositories;
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
            services.AddDbContext<ETicaretAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IOrderAsyncReadRepository, OrderAsyncReadRepository>();
            services.AddScoped<IOrderAsyncWriteRepository, OrderAsyncWriteRepository>();
            services.AddScoped<IProductAsyncReadRepository, ProductAsyncReadRepository>();
            services.AddScoped<IProductAsyncWriteRepository, ProductAsyncWriteRepository>();

        }
    }
}
