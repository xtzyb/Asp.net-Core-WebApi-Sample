using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZFS.Core.Interfaces
{
    public interface IUnitDbWork
    {
        Task<bool> SaveAsync();
    }
}
