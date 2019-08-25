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
    /// 系统菜单
    /// </summary>
    public class MenuController:BaseController
    {
        public readonly IMenuRepository repository;

        public MenuController(IUnitDbWork work, IMenuRepository repository)
            : base(work)
        {
            this.repository = repository;
        }
        
        /// <summary>
        /// 新增系统菜单
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMenu(Menu item)
        {
            repository.AddModel(item);
            await work.SaveAsync();
            return Ok();
        }
    }
}
