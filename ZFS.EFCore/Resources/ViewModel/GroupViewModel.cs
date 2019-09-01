using System;
using System.Collections.Generic;
using System.Text;

namespace ZFS.EFCore.Resources.ViewModel
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }

    public class GroupAddViewModel : GroupAddOrUpdateViewModel
    {

    }

    public class GroupUpdateViewModel : GroupAddOrUpdateViewModel
    {

    }


    public class GroupAddOrUpdateViewModel
    {
        public string GroupCode { get; set; }

        public string GroupName { get; set; }
    }

}
