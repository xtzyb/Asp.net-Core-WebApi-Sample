using System;
using System.Collections.Generic;
using System.Text;
using ZFS.Core.Entity;
using ZFS.Core.Interfaces;
using ZFS.EFCore.DbFile;

namespace ZFS.EFCore.Repositories
{
    public class DictionaryTypeRepository : BaseRepository<DictionaryType>, IDictionaryTypeRepository
    {
        public DictionaryTypeRepository(ZfsDbContext context) : base(context)
        {

        }
    }
}
