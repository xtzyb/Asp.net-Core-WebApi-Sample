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
    public class BaseRepository
    {
        public readonly ZfsDbContext context;

        public BaseRepository(ZfsDbContext context)
        {
            this.context = context;
        }
    }
}
