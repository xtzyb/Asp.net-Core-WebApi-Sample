using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ZFS.Core.Entity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ZFS.EFCore.DbFile.DbInit
{
    public class ZfsDbInitializer
    {
        public static async Task InitAsync(ZfsDbContext context,
            ILoggerFactory logger)
        {
            try
            {
                //服务第一次初始化, 默认所有表都是空的, 此处仅以单个表来判断数据库是否被初始化
                if (!context.Users.Any())
                {
                    #region 添加用户种子数据

                    var Users = new[] {
                    new User(){ Account="admin",UserName="管理员",Address="广州",Tel="", FlagAdmin='1', Password="123"},
                    new User(){ Account="test",UserName="测试员",Address="深圳",Tel="", FlagAdmin='1', Password="123"},
                    new User(){ Account="qc",UserName="质检部",Address="清远",Tel="", FlagAdmin='1', Password="123"},
                    new User(){ Account="pm",UserName="项目经理",Address="惠州",Tel="", FlagAdmin='1', Password="123"},
                    new User(){ Account="joy",UserName="阿杰",Address="USA",Tel="", FlagAdmin='1', Password="123"},
                    new User(){ Account="stive",UserName="斯蒂文",Address="USA",Tel="", FlagAdmin='1', Password="123"}
                };
                    foreach (var u in Users)
                    {
                        await context.Users.AddAsync(u);
                    }

                    #endregion

                    #region 添加功能定义种子数据

                    var Auths = new[]
                    {
                    new  Authorithitem(){ AuthorityName="添加", AuthorityValue="1" },
                     new  Authorithitem(){ AuthorityName="修改", AuthorityValue="2" },
                      new  Authorithitem(){ AuthorityName="删除", AuthorityValue="4" },
                       new  Authorithitem(){ AuthorityName="导入", AuthorityValue="8" }
                };

                    foreach (var item in Auths)
                    {
                        await context.Authorithitems.AddAsync(item);
                    }

                    #endregion

                    #region 添加数据字典数据

                    var Dictypes = new[]
                    {
                    new DictionaryType(){   TypeCode="1001", TypeName="重量" },
                    new DictionaryType(){   TypeCode="1002", TypeName="货币" },
                };

                    foreach (var item in Dictypes)
                    {
                        await context.DictionaryTypes.AddAsync(item);
                    }

                    var Dics = new[]
                    {
                    new Dictionaries(){   TypeCode="1001", NativeName="克", DataCode="g" },
                    new Dictionaries(){   TypeCode="1001", NativeName="毫克", DataCode="mg" },
                    new Dictionaries(){   TypeCode="1001", NativeName="千克", DataCode="kg" },
                    new Dictionaries(){   TypeCode="1002", NativeName="美元", DataCode="Dollar" },
                    new Dictionaries(){   TypeCode="1002", NativeName="欧元", DataCode="Eur" },
                    new Dictionaries(){   TypeCode="1002", NativeName="人民币", DataCode="Renminbi" },
                };

                    foreach (var item in Dics)
                    {
                        await context.Dictionaries.AddAsync(item);
                    }

                    #endregion

                    #region 添加用户组数据

                    var groups = new[]
                    {
                    new Group(){ GroupCode="admin_group", GroupName="管理员组" },
                     new Group(){ GroupCode="staff_group", GroupName="员工组" },
                };

                    foreach (var item in groups)
                    {
                        await context.Groups.AddAsync(item);
                    }

                    var menus = new[]
                    {
                    new Menu(){ MenuCode="userView", MenuName="用户管理",MenuCaption="管理用户信息",MenuNameSpace="ZFSDomain.ViewDialog.User ",MenuAuthorities=7,ParentName=""},
                    new Menu(){ MenuCode="groupView", MenuName="权限管理",MenuCaption="",MenuNameSpace="ZFSDomain.ViewDialog.Group",MenuAuthorities=7,ParentName=""},
                    new Menu(){ MenuCode="menuView", MenuName="菜单管理",MenuCaption="管理菜单数据",MenuNameSpace="ZFSDomain.ViewDialog.Menu",MenuAuthorities=7,ParentName=""},
                    new Menu(){ MenuCode="dictionaryView", MenuName="字典管理",MenuCaption="管理基础字典信息",MenuNameSpace="ZFSDomain.ViewDialog.Dictionary ",MenuAuthorities=7,ParentName=""},
                };

                    foreach (var item in menus)
                    {
                        await context.Menus.AddAsync(item);
                    }

                    #endregion

                    #region 设置相关的权限测试数据

                    var Groups = new[]
                    {
                        new Group(){ GroupCode="admin_group", GroupName="制造组" },
                        new Group(){ GroupCode="plan", GroupName="计划组" }
                    };

                    foreach (var item in Groups)
                    {
                        await context.Groups.AddAsync(item);
                    }

                    var GroupFunc = new[]
                    {
                        new GroupFunc(){ GroupCode="admin_group",MenuCode="userView",Authorities=0},
                        new GroupFunc() { GroupCode="admin_group",MenuCode="menuView",Authorities=0},
                        new GroupFunc() { GroupCode="admin_group",MenuCode="groupView",Authorities=0},
                        new GroupFunc() { GroupCode="admin_group",MenuCode="dictionaryView",Authorities=0},
                    };

                    foreach (var item in GroupFunc)
                    {
                        await context.GroupFuncs.AddAsync(item);
                    }

                    var GroupUser = new[]
                    {
                        new GroupUser(){ Account="admin", GroupCode="admin_group"},
                    };

                    foreach (var item in GroupUser)
                    {
                        await context.GroupUsers.AddAsync(item);
                    }

                    #endregion

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var log = logger.CreateLogger<ZfsDbContext>();
                log.LogError(ex.Message);
            }

        }
    }
}
