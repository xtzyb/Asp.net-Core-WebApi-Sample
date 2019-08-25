using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZFS.Core.Common;
using ZFS.Core.Entity;
using ZFS.Core.Query;

namespace ZFS.Core.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> LoginAsync(string account, string passWord);

        Task<object> GetPermissionByAccountAsync(string account);

        Task<PaginatedList<User>> GetAllUsersAsync(UserParameters parameters);

        Task<User> GetUserByIdAsync(int id);

        void AddPostAsync(User user);
        void Delete(User user);
        void Update(User user);
    }
}
