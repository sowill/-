using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CHAIN.BLL;
using Common;
using Telerik.Web.Mvc;
using System.Collections.Specialized;
using CHAIN.DAL;
using CHAIN.App.Models;

namespace CHAIN.App.Controllers
{
    public class historyController : Controller
    {
        //
        // GET: /history/
        hr_srv_historyBLL _historyBLL = new hr_srv_historyBLL();
        ValidationErrors validationErrors = new ValidationErrors();
        hr_srv_historyRepository _historyRepository = new hr_srv_historyRepository();
        SysEntities db = new SysEntities();

        public void BindDpl()
        {
            ViewBag.dplchuxing = hr.bindDpl("出行状态");
        }

        public ActionResult Index(FormCollection collection)
        {
            ViewData["employeename_search"] = collection["employeename_search"];
            ViewData["chuxing_search"] = collection["chuxing_search"];
            ViewData["chuxing_date_search"] = collection["chuxing_date_search"]??DateTime.Now.ToShortDateString();
            
            //ViewData["chuxing_date_search1"] = collection["chuxing_date_search1"];
            //ViewData["chuxing_date_search2"] = collection["chuxing_date_search2"];
            BindDpl();
            return View();
        }

        #region 对记录模块进行操作
        [GridAction]
        public ActionResult _Index()
        {

            NameValueCollection collection = new NameValueCollection(Request.QueryString);

            IEnumerable<CHAIN.DAL.hr_srv_history> history = _historyBLL.Bindhistory(collection);
            return View(new GridModel(history));
        }
        [GridAction]
        [HttpPost]
        public ActionResult _Insert_Edit(hr_srv_history item)
        {
            hr_srv_history historyDAL = new hr_srv_history();

            //更新数据
            if (TryUpdateModel(historyDAL))
            {
                if (item.id == 0)
                {
                    _historyBLL.Create(ref validationErrors, historyDAL);
                }
                else
                {
                    _historyBLL.Edit(ref validationErrors, historyDAL);
                }
            }   

            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<hr_srv_history> history = _historyBLL.Bindhistory(collection);
            return View(new GridModel(history));

        }

        [GridAction]
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            ///更加id删除某条数据
            _historyBLL.Delete(ref validationErrors, id);
            //hr_srv_history historyDAL = _historyRepository.GetById(id);


            /////找到子表，并删除子表的数据
            //int[] scoreArray = EducationinfoBLL.GetIdByMainId(id);
            //EducationinfoBLL.DeleteCollection(ref validationErrors, scoreArray);

            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<hr_srv_history> history = _historyBLL.Bindhistory(collection);
            return View(new GridModel(history));
        }

        #endregion
    }
}
