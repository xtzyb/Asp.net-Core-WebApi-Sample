using System;
using System.Collections.Generic;
using System.Text;
using ZFS.Core.Entity;
using ZFS.EFCore.Resources.ViewModel;
using ZFS.EFCore.Services.OrderBys;

namespace ZFS.EFCore.Resources.OrderByMapping
{
    public class DictionariesMapping : PropertyMapping<DictionariesViewModel, Dictionaries>
    {
        public DictionariesMapping() : base(
            new Dictionary<string, List<MappedProperty>>
                (StringComparer.OrdinalIgnoreCase)
            {
                [nameof(DictionariesViewModel.LastTime)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Dictionaries.LastUpdate), Revert = false}
                    },
                [nameof(DictionariesViewModel.LastTimeBy)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Dictionaries.LastUpdateBy), Revert = false}
                    },
                [nameof(DictionariesViewModel.CreateTime)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Dictionaries.CreationDate), Revert = false}
                    },
            })
        {

        }
    }
}
