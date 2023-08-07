using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDbContext _context;

        public ReadRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool changeTracking = true)
        {
            var query = Table.AsQueryable();
            if (!changeTracking) 
            { 
                return query.AsNoTracking(); 
            }

            return query;
        }

        public T GetById(string Id, bool changeTracking = true)
        {
            if (!changeTracking)
            {
                return Table.AsQueryable().AsNoTracking().FirstOrDefault(p=> p.Id == Guid.Parse(Id));
            }
           return Table.Find(Guid.Parse(Id));
        }

        public T GetSingle(Expression<Func<T, bool>> method, bool changeTracking = true)
        {
            if (!changeTracking)
            {
                return Table.AsQueryable().AsNoTracking().FirstOrDefault(method);
            }
            return Table.FirstOrDefault(method);

            
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool changeTracking = true)
        {
            var query = Table.Where(method);
            if (!changeTracking)
            {
                return query.AsNoTracking();
            }
            return query;
        }
    }
}
