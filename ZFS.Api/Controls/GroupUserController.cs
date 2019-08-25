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
    /// 组用户
    /// </summary>
    public class GroupUserController : BaseController
    {
        public readonly IGroupUserRepository repository;

        public GroupUserController(IUnitDbWork work, IGroupUserRepository repository)
            : base(work)
        {
            this.repository = repository;
        }
        

        /// <summary>
        /// 新增组用户
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddGroupUser(GroupUser item)
        {
            repository.AddModel(item);
            await work.SaveAsync();
            return Ok();
        }
    }
}
