using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZFS.Core.Entity;
using ZFS.Core.Interfaces;
using ZFS.EFCore.DbFile;
using Microsoft.EntityFrameworkCore;
using ZFS.Core.Common;
using ZFS.Core.Query;
using ZFS.EFCore.Services.OrderBys;
using ZFS.EFCore.Extensions;
using ZFS.EFCore.Resources.ViewModel;

namespace ZFS.EFCore.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IPropertyMappingContainer propertyMappingContainer;

        public UserRepository(ZfsDbContext context, IPropertyMappingContainer propertyMappingContainer) : base(context)
        {
            this.propertyMappingContainer = propertyMappingContainer;
        }

        public void AddPostAsync(User user)
        {
            context.Users.AddAsync(user);
        }

        public void Delete(User user)
        {
            context.Remove(user);
        }

        public async Task<PaginatedList<User>> GetAllUsersAsync(UserParameters parameters)
        {
            var query = context.Users.AsQueryable();
            //搜索,按登录名、用户名
            if (!string.IsNullOrEmpty(parameters.Search))
            {
                var serach = parameters.Search.ToLowerInvariant();
                query = query.Where(t => t.Account.ToLowerInvariant().Contains(serach) || t.UserName.ToLowerInvariant().Contains(serach));
            }

            //自定义排序
            query = query.ApplySort(parameters.OrderBy, propertyMappingContainer.Resolve<UserViewModel, User>());
            var count = await query.CountAsync();
            var data = await query.Skip(parameters.PageIndex - 1 * parameters.PageSize).Take(parameters.PageSize).ToListAsync();
            return new PaginatedList<User>(parameters.PageIndex - 1, parameters.PageSize, count, data);
        }

        public async Task<object> GetPermissionByAccountAsync(string account)
        {
            var data = from a in context.GroupFuncs
                       join b in context.GroupUsers on a.GroupCode equals b.GroupCode
                       join c in context.Groups on a.GroupCode equals c.GroupCode
                       join d in context.Menus on a.MenuCode equals d.MenuCode
                       select new
                       {
                           b.Account,
                           c.GroupName,
                           d.MenuName,
                           d.MenuCaption,
                           d.MenuNameSpace,
                           d.ParentName,
                           a.Authorities
                       };
            return await data.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<User> LoginAsync(string account, string passWord)
        {
            var result = await context.Users.FirstOrDefaultAsync(t => t.Account == account && t.Password == passWord);
            return result;
        }

        public void Update(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }
    }
}
