using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool changeTracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool changeTracking = true);
        T GetSingle(Expression<Func<T, bool>> method, bool changeTracking = true);
        T GetById(string Id, bool changeTracking = true);
    }
}
