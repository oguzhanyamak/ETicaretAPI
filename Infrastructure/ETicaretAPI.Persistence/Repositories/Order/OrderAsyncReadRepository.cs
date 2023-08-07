using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class OrderAsyncReadRepository : AsyncReadRepository<Order>, IOrderAsyncReadRepository
    {
        public OrderAsyncReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
