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
    /// 字典类型
    /// </summary>
    public class DictionaryTypeController : BaseController
    {
        public readonly IDictionaryTypeRepository repository;

        public DictionaryTypeController(IUnitDbWork work, IDictionaryTypeRepository repository)
            : base(work)
        {
            this.repository = repository;
        }
        
    }
}
