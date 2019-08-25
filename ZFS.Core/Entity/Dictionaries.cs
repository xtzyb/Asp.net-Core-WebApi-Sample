using System;
using System.Collections.Generic;
using System.Text;

namespace ZFS.Core.Entity
{
    public class Dictionaries : BaseEntity
    {
        public string TypeCode { get; set; }

        public string DataCode { get; set; }

        public string NativeName { get; set; }

        public string EnglishName { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime LastUpdate { get; set; }

        public string LastUpdateBy { get; set; }
    }
}
