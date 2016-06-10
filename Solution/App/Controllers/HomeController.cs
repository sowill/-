using System.Web.Mvc;
using System.Collections.Generic;
using CHAIN.DAL;
using CHAIN.BLL;
using Common;
using CHAIN.App.Models;
namespace CHAIN.App.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            Account account = GetCurrentAccount();
            if (account == null)
            {
                RedirectToAction("Index", "Account");
            }
            else
            {
                ViewData["PersonName"] = account.Name;
                using (HomeBLL home = new HomeBLL())
                {
                    ViewData["Menu"] = home.GetMenuByPersonId(account.Id);// 获取菜单
                }
            }

            return View();
        }
        /// <summary>
        /// 根据父节点获取数据字典,绑定二级下拉框的时候使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetSysFieldByParent(string id, string parentid, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            using (SysFieldHander baseDDL = new SysFieldHander())
            {
                return Json(new SelectList(baseDDL.GetSysFieldByParent(id, parentid, value), "MyTexts", "MyTexts"), JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// 获取列表中的按钮导航
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetToolbar(string id, string action = "Index")
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            List<toolbar> toolbars = new List<toolbar>();
            switch (id)
            {
                case "SysRole":
                    toolbars.Add(new toolbar() { handler = "setSysMenu", iconCls = "icon-add", text = "设置" });
                      break;
                default:
                    break;
            }
            toolbars.Add(new toolbar() { handler = "getView", iconCls = "icon-details", text = "详细" });
            toolbars.Add(new toolbar() { handler = "flexiCreate", iconCls = "icon-add", text = "创建" });
            toolbars.Add(new toolbar() { handler = "flexiModify", iconCls = "icon-edit", text = "修改" });
            toolbars.Add(new toolbar() { handler = "flexiDelete", iconCls = "icon-remove", text = "删除" });

            return Json(toolbars, JsonRequestBehavior.AllowGet);
        }
    }
}


