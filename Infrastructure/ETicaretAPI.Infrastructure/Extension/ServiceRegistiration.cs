using ETicaretAPI.Application.Abstraction.GoogleAuth;
using ETicaretAPI.Application.Abstraction.Token;
using ETicaretAPI.Infrastructure.Services.GoogleAuth;
using ETicaretAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Extension
{
    public static class ServiceRegistiration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IGoogleAuth, GoogleAuthService>();
        }
    }
}
