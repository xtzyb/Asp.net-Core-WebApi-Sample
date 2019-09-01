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
using ZFS.EFCore.Extensions;
using ZFS.EFCore.Resources.ViewModel;
using ZFS.EFCore.Services.FilterVerification;
using ZFS.EFCore.Services.OrderBys;

namespace ZFS.Api.Controls
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    public class MenuController : BaseController
    {

        public readonly IMenuRepository repository;
        private readonly IMapper mapper;
        private readonly ITypeHelperService typeHelperService;
        private readonly IPropertyMappingContainer propertyMappingContainer;

        /// <summary>
        ///  
        /// </summary>
        /// <param name="work">DB Work</param>
        /// <param name="repository">业务实例</param>
        /// <param name="mapper">映射</param>
        /// <param name="typeHelperService">字段验证</param>
        /// <param name="propertyMappingContainer">排序验证</param>
        public MenuController(IUnitDbWork work,
            IMenuRepository repository,
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
        ///  根据用户姓名获取用户数据列表
        /// </summary>
        /// <param name="parameters">搜索</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMenu([FromQuery] MenuParameters parameters)
        {
            try
            {
                //验证排序的字段是否存在
                if (!propertyMappingContainer.ValidateMappingExistsFor<MenuViewModel, Menu>(parameters.OrderBy))
                    return BadRequest("order by Fidled not exist");

                //验证过滤的字段是否存在
                if (!typeHelperService.TypeHasProperties<MenuViewModel>(parameters.Fields))
                    return BadRequest("fidled not exist");

                var users = await repository.GetAllGroupsAsync(parameters);
                var userViewModel = mapper.Map<IEnumerable<Menu>, IEnumerable<MenuViewModel>>(users);

                var shapedViewModel = userViewModel.ToDynamicIEnumerable(parameters.Fields);

                return Ok(new BaseResponse() { success = true, dynamicObj = shapedViewModel, TotalRecord = users.TotalItemsCount });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse() { success = false, message = ex.Message });
            }
        }
    }
}
