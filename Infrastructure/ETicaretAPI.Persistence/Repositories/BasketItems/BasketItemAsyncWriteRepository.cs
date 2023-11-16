using ETicaretAPI.Application.Repositories.BasketItems;
using ETicaretAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.BasketItems
{
    public class BasketItemAsyncWriteRepository : AsyncWriteRepository<Domain.Entities.BasketItem>, IBasketItemAsyncWriteRepository
    {
        public BasketItemAsyncWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
