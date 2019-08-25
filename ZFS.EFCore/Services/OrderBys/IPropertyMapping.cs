using System;
using System.Collections.Generic;
using System.Text;

namespace ZFS.EFCore.Services.OrderBys
{
    public interface IPropertyMapping
    {
        Dictionary<string, List<MappedProperty>> MappingDictionary { get; }
    }
}
