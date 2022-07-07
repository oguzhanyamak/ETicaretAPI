using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application
{
    public static class ServiceRegistiration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddMediatR(typeof(ServiceRegistiration));
            collection.AddHttpClient();
            //ServiceRegistiration'ın bulunduğu assembly deki tüm handler sınıflarını içeri aktarır
        }
    }
}
