using System;
using System.Collections.Generic;
using System.Text;

namespace ZFS.EFCore.Services.FilterVerification
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}
