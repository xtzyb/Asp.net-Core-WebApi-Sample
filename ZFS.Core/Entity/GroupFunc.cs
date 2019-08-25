using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZFS.Core.Entity
{
    public class GroupFunc : BaseEntity
    {
        public string GroupCode { get; set; }

        public string MenuCode { get; set; }

        [DefaultValue(0)]
        public int Authorities { get; set; }
    }
}
