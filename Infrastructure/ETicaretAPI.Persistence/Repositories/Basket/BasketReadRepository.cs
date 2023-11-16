using ETicaretAPI.Application.Repositories.Basket;
using ETicaretAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.Basket
{
    public class BasketReadRepository : ReadRepository<Domain.Entities.Basket>, IBasketReadRepository
    {
        public BasketReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
