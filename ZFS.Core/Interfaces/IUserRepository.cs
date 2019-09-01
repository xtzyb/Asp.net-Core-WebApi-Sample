using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZFS.Core.Common;
using ZFS.Core.Entity;
using ZFS.Core.Query;

namespace ZFS.Core.Interfaces
{
    public interface IUserRepository 
    {
        Task<User> LoginAsync(string account, string passWord);

        Task<object> GetPermissionByAccountAsync(string account);

        Task<PaginatedList<User>> GetAllUsersAsync(UserParameters parameters);

        Task<User> GetUserByIdAsync(int id);

        void AddUserAsync(User user);
        void DeleteAsync(User user);
        void UpdateAsync(User user);
    }
}
