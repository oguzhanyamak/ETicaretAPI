using ETicaretAPI.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.SignalR.Extension
{
    public static class HubRegistirations
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<ProductHub>("/product-hub");
            webApplication.MapHub<OrderHub>("/orders-hub");
        }
    }
}
