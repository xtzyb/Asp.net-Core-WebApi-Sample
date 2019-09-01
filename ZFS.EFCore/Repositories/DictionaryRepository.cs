using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Core.Common;
using ZFS.Core.Entity;
using ZFS.Core.Interfaces;
using ZFS.Core.Query;
using ZFS.EFCore.DbFile;
using ZFS.EFCore.Extensions;
using ZFS.EFCore.Resources.ViewModel;
using ZFS.EFCore.Services.OrderBys;

namespace ZFS.EFCore.Repositories
{

    public class DictionaryRepository : BaseRepository, IDictionaryRepository
    {
        private readonly IPropertyMappingContainer propertyMappingContainer;

        public DictionaryRepository(ZfsDbContext context, IPropertyMappingContainer propertyMappingContainer) : base(context)
        {
            this.propertyMappingContainer = propertyMappingContainer;
        }

        public async Task<PaginatedList<Dictionaries>> GetAllDicAsync(DictionariesParameters parameters)
        {
            var query = context.Dictionaries.AsQueryable();
            //搜索,按登录名、用户名
            if (!string.IsNullOrEmpty(parameters.Search))
            {
                var search = parameters.Search.ToLowerInvariant();
                query = query.Where(t => t.DataCode.ToLowerInvariant().Contains(search) ||
                    t.EnglishName.ToLowerInvariant().Contains(search) ||
                    t.NativeName.ToLowerInvariant().Contains(search)
                );
            }

            //自定义排序
            query = query.ApplySort(parameters.OrderBy, propertyMappingContainer.Resolve<DictionariesViewModel, Dictionaries>());
            var count = await query.CountAsync();
            var data = await query.Skip(parameters.PageIndex - 1 * parameters.PageSize).Take(parameters.PageSize).ToListAsync();
            return new PaginatedList<Dictionaries>(parameters.PageIndex - 1, parameters.PageSize, count, data);
        }
    }
}
