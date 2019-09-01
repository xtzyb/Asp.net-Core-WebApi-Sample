using System;
using System.Collections.Generic;
using System.Text;
using ZFS.Core.Entity;
using ZFS.EFCore.Resources.ViewModel;
using ZFS.EFCore.Services.OrderBys;

namespace ZFS.EFCore.Resources.OrderByMapping
{
    public class GroupPropertyMappting : PropertyMapping<GroupViewModel, Group>
    {
        public GroupPropertyMappting() : base(
            new Dictionary<string, List<MappedProperty>>
                (StringComparer.OrdinalIgnoreCase)
            {
                [nameof(GroupViewModel.Code)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Group.GroupCode), Revert = false}
                    },
                [nameof(GroupViewModel.Name)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Group.GroupName), Revert = false}
                    },
            })
        {

        }
    }
}
