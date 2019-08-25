using System;
using System.Collections.Generic;
using System.Text;
using ZFS.Core.Entity;
using ZFS.EFCore.Resources.ViewModel;
using ZFS.EFCore.Services.OrderBys;

namespace ZFS.EFCore.Resources.OrderByMapping
{
    public class UserPropertyMapping : PropertyMapping<UserViewModel, User>
    {
        public UserPropertyMapping() : base(
            new Dictionary<string, List<MappedProperty>>
                (StringComparer.OrdinalIgnoreCase)
            {
                [nameof(UserViewModel.LastTime)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(User.LastLoginTime), Revert = false}
                    },
                [nameof(UserViewModel.EndTime)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(User.LastLogouTime), Revert = false}
                    },
            })
        {

        }
    }
}
