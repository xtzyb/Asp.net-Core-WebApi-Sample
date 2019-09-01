using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZFS.Core.Entity;
using ZFS.Core.Interfaces;
using ZFS.EFCore.DbFile;
using Microsoft.EntityFrameworkCore;
using ZFS.Core.Common;
using ZFS.Core.Query;
using ZFS.EFCore.Services.OrderBys;
using ZFS.EFCore.Extensions;
using ZFS.EFCore.Resources.ViewModel;


namespace ZFS.EFCore.Repositories
{
    public class GroupRepository : BaseRepository, IGroupRepository
    {
        private readonly IPropertyMappingContainer propertyMappingContainer;

        public GroupRepository(ZfsDbContext context, IPropertyMappingContainer propertyMappingContainer) : base(context)
        {
            this.propertyMappingContainer = propertyMappingContainer;
        }

        public async Task<PaginatedList<Group>> GetAllGroupsAsync(GroupParameters parameters)
        {
            var query = context.Groups.AsQueryable();
            if (!string.IsNullOrEmpty(parameters.Search))
            {
                var serach = parameters.Search.ToLowerInvariant();
                query = query.Where(t => t.GroupCode.ToLowerInvariant().Contains(serach) || t.GroupName.ToLowerInvariant().Contains(serach));
            }
            
            query = query.ApplySort(parameters.OrderBy, propertyMappingContainer.Resolve<GroupViewModel, Group>());
            var count = await query.CountAsync();
            var data = await query.Skip(parameters.PageIndex - 1 * parameters.PageSize).Take(parameters.PageSize).ToListAsync();
            return new PaginatedList<Group>(parameters.PageIndex - 1, parameters.PageSize, count, data);
        }
    }
}
