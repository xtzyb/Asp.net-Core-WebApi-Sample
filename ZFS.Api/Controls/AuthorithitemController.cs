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
        

        /// <summary>
        /// 新增权限定义
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost, SwgApi]
        public async Task<IActionResult> AddAuthItem(Authorithitem item)
        {
            repository.AddModel(item);
            await work.SaveAsync();
            return Ok();
        }
    }
}
