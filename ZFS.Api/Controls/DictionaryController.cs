using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZFS.Core.Common;
using ZFS.Core.Entity;
using ZFS.Core.Interfaces;
using ZFS.Core.Query;
using ZFS.EFCore.Resources.ViewModel;
using ZFS.EFCore.Services.FilterVerification;
using ZFS.EFCore.Services.OrderBys;

namespace ZFS.Api.Controls
{
    /// <summary>
    /// 字典数据
    /// </summary>
    
public class DictionaryController : BaseController
    {
        public readonly IDictionaryRepository repository;
        private readonly IMapper mapper;
        private readonly ITypeHelperService typeHelperService;
        private readonly IPropertyMappingContainer propertyMappingContainer;

        /// <summary>
        ///  用户构造函数
        /// </summary>
        /// <param name="work">DB Work</param>
        /// <param name="repository">业务实例</param>
        /// <param name="mapper">映射</param>
        /// <param name="typeHelperService">字段验证</param>
        /// <param name="propertyMappingContainer">排序验证</param>
        public DictionaryController(IUnitDbWork work,
            IDictionaryRepository repository,
            IMapper mapper,
            ITypeHelperService typeHelperService,
            IPropertyMappingContainer propertyMappingContainer
            )
            : base(work)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.typeHelperService = typeHelperService;
            this.propertyMappingContainer = propertyMappingContainer;
        }

        /// <summary>
        /// 获取字典数据列表
        /// </summary>
        /// <param name="parameters">搜索</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDictionaries([FromQuery] DictionariesParameters parameters)
        {
            try
            {
                //验证排序的字段是否存在
                if (!propertyMappingContainer.ValidateMappingExistsFor<DictionariesViewModel, Dictionaries>(parameters.OrderBy))
                    return BadRequest("order by Fidled not exist");

                //验证过滤的字段是否存在
                if (!typeHelperService.TypeHasProperties<DictionariesViewModel>(parameters.Fields))
                    return BadRequest("fidled not exist");

                var users = await repository.GetAllDicAsync(parameters);
                var userViewModel = mapper.Map<IEnumerable<Dictionaries>, IEnumerable<DictionariesViewModel>>(users);
                return Ok(new BaseResponse() { success = true, dynamicObj = userViewModel,TotalRecord=users.TotalItemsCount });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse() { success = false, message = ex.Message });
            }
        }
        
    }
}
