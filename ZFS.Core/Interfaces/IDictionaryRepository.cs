using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZFS.Core.Common;
using ZFS.Core.Entity;
using ZFS.Core.Query;

namespace ZFS.Core.Interfaces
{
    public interface IDictionaryRepository : IBaseRepository<Dictionaries>
    {
        Task<PaginatedList<Dictionaries>> GetAllDicAsync(DictionariesParameters parameters);
    }
}
