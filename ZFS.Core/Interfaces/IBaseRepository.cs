using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZFS.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetModels();

        void AddModel(T model);

        void UpdateModel(T model);

        void DeleteModel(T model);
    }
}
