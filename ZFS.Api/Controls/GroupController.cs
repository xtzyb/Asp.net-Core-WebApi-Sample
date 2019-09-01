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
    /// 用户组
    /// </summary>
    public class GroupController : BaseController
    {
        public readonly IGroupRepository repository;
        private readonly IMapper mapper;
        private readonly ITypeHelperService typeHelperService;
        private readonly IPropertyMappingContainer propertyMappingContainer;

        /// <summary>
        ///  用户组构造函数
        /// </summary>
        /// <param name="work">DB Work</param>
        /// <param name="repository">业务实例</param>
        /// <param name="mapper">映射</param>
        /// <param name="typeHelperService">字段验证</param>
        /// <param name="propertyMappingContainer">排序验证</param>
        public GroupController(IUnitDbWork work,
            IGroupRepository repository,
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
        public async Task<IActionResult> GetGroup([FromQuery] GroupParameters parameters)
        {
            try
            {
                //验证排序的字段是否存在
                if (!propertyMappingContainer.ValidateMappingExistsFor<GroupViewModel, Group>(parameters.OrderBy))
                    return BadRequest("order by Fidled not exist");

                //验证过滤的字段是否存在
                if (!typeHelperService.TypeHasProperties<GroupViewModel>(parameters.Fields))
                    return BadRequest("fidled not exist");

                var users = await repository.GetAllGroupsAsync(parameters);
                var userViewModel = mapper.Map<IEnumerable<Group>, IEnumerable<GroupViewModel>>(users);

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
