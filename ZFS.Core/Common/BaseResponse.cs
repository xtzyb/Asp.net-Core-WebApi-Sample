using System;
using System.Collections.Generic;
using System.Text;

namespace ZFS.Core.Common
{
    public class BaseResponse
    {
        /// <summary>
        /// 后台消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// //返回状态，正常是200，如果是407 说明无权限需要跳转登录界面
        /// </summary>
        public int statusCode { get; set; }

        /// <summary>
        /// //分页的时候指定每页显示多少条数据
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int totalPage { get; set; }

        /// <summary>
        /// 是否分页
        /// </summary>
        public string paging { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// 总共的记录数
        /// </summary>
        public int TotalRecord { get; set; }

        public dynamic dynamicObj { get; set; }
    }
}
