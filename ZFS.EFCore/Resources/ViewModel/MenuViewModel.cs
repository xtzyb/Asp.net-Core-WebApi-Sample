using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZFS.EFCore.Resources.ViewModel
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Caption { get; set; }

        public string NameSpace { get; set; }

        [DefaultValue(0)]
        public int Authorities { get; set; }

        public string ParentName { get; set; }
    }
}
