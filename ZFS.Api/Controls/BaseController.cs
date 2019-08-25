using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZFS.Core.Interfaces;

namespace ZFS.Api.Controls
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : Controller 
    {
        public readonly IUnitDbWork work;
       
        public BaseController(IUnitDbWork work)
        {
            this.work = work;
        }
    }
}
