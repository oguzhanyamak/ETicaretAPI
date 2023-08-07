using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        readonly private ETicaretAPIDbContext _context;

        public WriteRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public bool Add(T model)
        {
            EntityEntry result = Table.Add(model);
            if (result.State == EntityState.Added)
                return true;
            else
                return false;
        }

        public bool AddRange(List<T> datas)
        {
            Table.AddRange(datas);
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry result = Table.Remove(model);
            if (result.State == EntityState.Deleted)
                return true;
            else
                return false;
        }

        public bool Remove(string Id)
        {
            T? model = Table.FirstOrDefault(data => data.Id == Guid.Parse(Id));
            EntityEntry result =Table.Remove(model);
            if(result.State ==EntityState.Deleted)
                return true;
            else
                return false;
        }

        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool Update(T Model)
        {
            EntityEntry result = Table.Update(Model);
            if (result.State == EntityState.Modified)
                return true;
            else
                return false;
        }
    }
}
