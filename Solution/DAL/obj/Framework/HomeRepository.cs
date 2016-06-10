using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace CHAIN.DAL
{
    /// <summary>
    /// 框架页
    /// </summary>
    public class HomeRepository
    {
        /// <summary>
        /// 根据PersonId获取已经启用的菜单
        /// </summary>
        /// <param name="personId">人员的Id</param>
        /// <returns>菜单拼接的字符串</returns>
        public string GetMenuByPersonId(string personId)
        {
            using (SysEntities db = new SysEntities())
            {
                StringBuilder strmenu = new StringBuilder();
                List<SysMenu> menuNeed =
                            (

                                    from m2 in db.SysMenu
                                    from r2 in m2.SysRole
                                    from p2 in r2.SysPerson
                                    where p2.Id == personId
                                    ////where m2.State == "开启"
                                    select m2
                            ).ToList();
                //拼接菜单的字符串
                if (menuNeed != null && menuNeed.Any())
                {
                    //一级节点
                    var menuid = from f in menuNeed
                                 where f.ParentId == null
                                 orderby f.Sort
                                 select f;

                    if (menuid != null && menuid.Any())
                    {
                        //二级节点
                        IList<SysMenu> menus = menuNeed.Except(menuid).OrderBy(o => o.Sort).ToList();
                        foreach (var item in menuid)
                        {
                            if (!string.IsNullOrWhiteSpace(item.Iconic))
                            {
                                strmenu.Append(string.Format(" <div data-options=@iconCls:'{0}'@ title=@{1}@>", item.Iconic, item.Name));
                            }
                            else
                            {
                                strmenu.Append(" <div title=@" + item.Name + "@>");
                            }
                            strmenu.Append(" <div class=@easyui-panel@ fit=@true@ border=@false@>");//解决ie6下没有滚动条的问题  
                            string menusString = GetMenus(item.Id, menus);
                            if (!string.IsNullOrWhiteSpace(menusString))
                            {
                                menusString = "<ul class=@easyui-tree@" + menusString.Substring(3);
                            }
                            strmenu.Append(menusString);
                            strmenu.Append(" </div>");
                            strmenu.Append(" </div>");//解决ie6下没有滚动条的问题
                        }
                    }
                }
                return strmenu.ToString().Replace('@', '"');
            }
        }
        /// <summary>
        /// 获取树形结构的字符串
        /// </summary>
        StringBuilder strmenu2 = new StringBuilder();
        /// <summary>
        /// 获取树形结构
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="sysMenus"></param>
        /// <returns></returns>
        protected string GetMenus(string menuId, IList<SysMenu> sysMenus)
        {
            string res = string.Empty;
            var listTree = from f in sysMenus
                           where f.ParentId == menuId
                           orderby f.Sort
                           select f;

            if (listTree != null && listTree.Any())
            {//填充数据

                strmenu2.Append("<ul>");
                foreach (var item in listTree)
                {
                    List<string> dataoptions = new List<string>();
                    if (!string.IsNullOrWhiteSpace(item.Iconic))
                    {
                        dataoptions.Add(string.Format("iconCls:'{0}'", item.Iconic));
                    }
                    if (item.SysMenu1.Any())
                    {
                        ////此处可以在数据字典中配置，将State字段，配置为下拉框，下拉框其中有一个值是"关闭"
                        if (!string.IsNullOrWhiteSpace(item.State) && item.State == "关闭")
                        {//菜单默认显示关闭
                            dataoptions.Add(string.Format("state:'closed'"));
                        }

                        strmenu2.Append(string.Format("<li data-options=@{0}@>", string.Join(",", dataoptions)));
                        strmenu2.Append("<span>" + item.Name + "</span> ");
                        var getmenu = GetMenus(item.Id, sysMenus);
                        strmenu2.Append(getmenu);

                        strmenu2.Append("</li>");
                    }
                    else
                    {
                        strmenu2.Append(string.Format("<li data-options=@{0}@>", string.Join(",", dataoptions)));
                        strmenu2.Append("<a href=@#@ icon=@" + item.Iconic + "@ rel=@" + item.Url + "@ >" + item.Name + "</a>");
                        strmenu2.Append("</li>");
                    }
                }
                strmenu2.Append("</ul>");
            }
            res = strmenu2.ToString();
            strmenu2.Clear();
            return res;
        }
    }
}



