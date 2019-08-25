using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Api.Design;
using ZFS.Core.Common;
using ZFS.Core.Entity;
using ZFS.Core.Interfaces;
using ZFS.Core.Query;
using ZFS.EFCore.DbFile;
using ZFS.EFCore.Resources.ViewModel;
using ZFS.EFCore.Services.FilterVerification;
using ZFS.EFCore.Services.OrderBys;

namespace ZFS.Api.Controls
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : BaseController
    {
        public readonly IUserRepository repository;
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
        public UserController(IUnitDbWork work,
            IUserRepository repository,
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
        public async Task<IActionResult> GetUser([FromQuery] UserParameters parameters)
        {
            try
            {
                //验证排序的字段是否存在
                if (!propertyMappingContainer.ValidateMappingExistsFor<UserViewModel, User>(parameters.OrderBy))
                    return BadRequest("order by Fidled not exist");

                //验证过滤的字段是否存在
                if (!typeHelperService.TypeHasProperties<UserViewModel>(parameters.Fields))
                    return BadRequest("fidled not exist");

                var users = await repository.GetAllUsersAsync(parameters);
                var userViewModel = mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);
                return Ok(new BaseResponse() { success = true, dynamicObj = userViewModel, TotalRecord = users.TotalItemsCount });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse() { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login(string account, string passWord)
        {
            try
            {
                var users = await repository.LoginAsync(account, passWord);
                return Ok(new BaseResponse() { success = true, dynamicObj = users, message = users == null ? "账号或密码错误!" : "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse() { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="viewModel">新增用户实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserAddViewModel viewModel)
        {
            if (viewModel == null)
            {
                return Ok(new BaseResponse() { success = false, message = "提交的数据有误!" });
            }

            try
            {
                var newUser = mapper.Map<UserAddViewModel, User>(viewModel); //Map转换
                newUser.Password = CEncoder.Encode(newUser.Password);
                newUser.CreateTime = DateTime.Now;
                newUser.LastLoginTime = DateTime.Now;
                newUser.LastLogouTime = DateTime.Now;
                repository.AddModel(newUser);

                if (!await work.SaveAsync())
                {
                    return Ok(new BaseResponse() { success = false, message = "save error!" });
                }
                return Ok(new BaseResponse() { success = true });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse() { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="viewModel">更新实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateViewModel viewModel)
        {
            if (viewModel == null)
            {
                return Ok(new BaseResponse() { success = false, message = "提交的数据有误!" });
            }
            try
            {
                var user = await repository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return Ok(new BaseResponse() { success = false, message = "未能找到该用户信息!" });
                }

                mapper.Map(viewModel, user);

                if (!await work.SaveAsync())
                {
                    return Ok(new BaseResponse() { success = false, message = $"Updating post {id} failed when saving." });
                }
                return Ok(new BaseResponse() { success = true });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse() { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var post = await repository.GetUserByIdAsync(id);
                if (post == null)
                {
                    return Ok(new BaseResponse() { success = false, message = "未能找到该用户信息!" });
                }
                repository.Delete(post);

                if (!await work.SaveAsync())
                {
                    return Ok(new BaseResponse() { success = false, message = $"Deleting post {id} failed when saving." });
                }
                return Ok(new BaseResponse() { success = true });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse() { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 获取用户的权限
        /// </summary>
        /// <param name="account">账号名</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Perm(string account)
        {
            try
            {
                var result = await repository.GetPermissionByAccountAsync(account);
                return Ok(new BaseResponse() { success = true, dynamicObj = result });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse() { success = false, message = ex.Message });
            }
        }
    }
}
