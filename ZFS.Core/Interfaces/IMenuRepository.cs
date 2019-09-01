using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZFS.Core.Common;
using ZFS.Core.Entity;
using ZFS.Core.Query;

namespace ZFS.Core.Interfaces
{
    public interface IMenuRepository 
    {
        Task<PaginatedList<Menu>> GetAllGroupsAsync(MenuParameters parameters);
    }
}
