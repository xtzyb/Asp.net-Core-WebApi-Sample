using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZFS.Core.Entity;
using ZFS.Core.Interfaces;

namespace ZFS.Api.Controls
{
    /// <summary>
    /// 用户组
    /// </summary>
    public class GroupController : BaseController
    {
        public readonly IGroupRepository repository;

        public GroupController(IUnitDbWork work, IGroupRepository repository)
            : base(work)
        {
            this.repository = repository;
        }
        
        /// <summary>
        /// 新增用户组
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddGroup(Group item)
        {
            repository.AddModel(item);
            await work.SaveAsync();
            return Ok();
        }
    }
}
