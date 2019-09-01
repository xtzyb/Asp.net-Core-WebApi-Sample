using System;
using System.Collections.Generic;
using System.Text;
using ZFS.Core.Entity;
using ZFS.Core.Interfaces;
using ZFS.EFCore.DbFile;

namespace ZFS.EFCore.Repositories
{
    public class AuthorithitemRepository : BaseRepository, IAuthorithitemRepository
    {
        public AuthorithitemRepository(ZfsDbContext context) : base(context)
        {

        }
    }
}
