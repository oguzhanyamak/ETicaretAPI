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
    public class ProductAsyncWriteRepository : AsyncWriteRepository<Product>, IProductAsyncWriteRepository
    {
        public ProductAsyncWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
