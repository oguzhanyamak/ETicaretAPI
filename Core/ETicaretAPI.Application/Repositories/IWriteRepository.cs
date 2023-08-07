using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        bool Add(T model);
        bool AddRange(List<T> datas);
        bool Remove(T model);
        bool Remove(string Id);
        bool RemoveRange(List<T> datas);
        bool Update(T Model);
        int Save();
    }
}
