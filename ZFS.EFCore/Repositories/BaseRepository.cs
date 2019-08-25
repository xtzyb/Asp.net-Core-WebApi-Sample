using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZFS.EFCore.DbFile;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ZFS.EFCore.Repositories
{
    public class BaseRepository<T> where T : class
    {
        public readonly ZfsDbContext context;

        public BaseRepository(ZfsDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<T>> GetModels()
        {
            return await context.Set<T>().ToListAsync();
        }

        public void AddModel(T model)
        {
            context.Set<T>().Add(model);
        }

        public void DeleteModel(T model)
        {
            context.Entry<T>(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void UpdateModel(T model)
        {
            EntityEntry entry = context.Entry<T>(model);
            entry.State = EntityState.Modified;
        }
    }
}
