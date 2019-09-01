using System;
using System.Collections.Generic;
using System.Text;
using ZFS.Core.Entity;
using ZFS.EFCore.Resources.ViewModel;
using ZFS.EFCore.Services.OrderBys;

namespace ZFS.EFCore.Resources.OrderByMapping
{
    public class MenuPropertyMapping : PropertyMapping<MenuViewModel, Menu>
    {
        public MenuPropertyMapping() : base(
            new Dictionary<string, List<MappedProperty>>
                (StringComparer.OrdinalIgnoreCase)
            {
                [nameof(MenuViewModel.Code)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Menu.MenuCode), Revert = false}
                    },
                [nameof(MenuViewModel.Name)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Menu.MenuName), Revert = false}
                    },
                [nameof(MenuViewModel.Caption)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Menu.MenuCaption), Revert = false}
                    },
                [nameof(MenuViewModel.Authorities)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Menu.MenuAuthorities), Revert = false}
                    },
            })
        {

        }
    }
}
