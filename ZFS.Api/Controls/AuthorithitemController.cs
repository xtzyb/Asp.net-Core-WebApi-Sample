using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZFS.Core.Common;
using ZFS.Core.Entity;
using ZFS.Core.Interfaces;

namespace ZFS.Api.Controls
{
    
    /// <summary>
    /// 权限定义
    /// </summary>
    public class AuthorithitemController : BaseController
    {
        public readonly IAuthorithitemRepository repository;

        public AuthorithitemController(IUnitDbWork work, IAuthorithitemRepository repository)
            : base(work)
        {
            this.repository = repository;
        }
        
    }
}
