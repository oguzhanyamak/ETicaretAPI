using ETicaretAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence
{
    public class DesingTimeDbContextFactory : IDesignTimeDbContextFactory<ETicaretAPIDbContext>
    {
        public ETicaretAPIDbContext CreateDbContext(string[] args)
        {

            DbContextOptionsBuilder<ETicaretAPIDbContext> dbContextBuilder = new();
            dbContextBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(dbContextBuilder.Options);
        }
    }
}
