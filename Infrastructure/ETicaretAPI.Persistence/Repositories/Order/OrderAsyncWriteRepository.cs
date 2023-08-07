using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class OrderAsyncWriteRepository : AsyncWriteRepository<Order>, IOrderAsyncWriteRepository
    {
        public OrderAsyncWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
