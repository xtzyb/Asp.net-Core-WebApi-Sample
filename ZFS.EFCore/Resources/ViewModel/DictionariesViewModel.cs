using System;
using System.Collections.Generic;
using System.Text;

namespace ZFS.EFCore.Resources.ViewModel
{
    public class DictionariesViewModel
    {
        public string TypeCode { get; set; }

        public string DataCode { get; set; }

        public string NativeName { get; set; }

        public string EnglishName { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateBy { get; set; }

        public DateTime LastTime { get; set; }

        public string LastTimeBy { get; set; }
    }
}
