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
    public class partController : Controller
    {
        //
        // GET: /part/
        hr_pam_partBLL _partBLL = new hr_pam_partBLL();
        ValidationErrors validationErrors = new ValidationErrors();
        hr_pam_partRepository _partRepository = new hr_pam_partRepository();
        SysEntities db = new SysEntities();

        public void BindDpl()
        {
            ViewBag.dpldepartmentname = hr.bindDpl("部门名称");
        }

        public ActionResult Index(FormCollection collection)
        {
            ViewData["employeename_search"] = collection["employeename_search"];
            ViewData["departmentname_search"] = collection["departmentname_search"];
            BindDpl();
            return View();
        }

        #region 隶属关系模块操作

        [GridAction]
        public ActionResult _Index()
        {

            NameValueCollection collection = new NameValueCollection(Request.QueryString);

            IEnumerable<CHAIN.DAL.hr_pam_part> part = _partBLL.Bindpart(collection);
            return View(new GridModel(part));
        }
        [GridAction]
        [HttpPost]
        public ActionResult _Insert_Edit(hr_pam_part item)
        {
            hr_pam_part partDAL = new hr_pam_part();

            //更新数据
            if (TryUpdateModel(partDAL))
            {
                if (item.id == 0)
                {
                    _partBLL.Create(ref validationErrors, partDAL);
                }
                else
                {
                    _partBLL.Edit(ref validationErrors, partDAL);
                }
            }

            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<hr_pam_part> part = _partBLL.Bindpart(collection);
            return View(new GridModel(part));
        }
        [GridAction]
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            ///更加id删除某条数据
            _partBLL.Delete(ref validationErrors, id);
            //hr_srv_history historyDAL = _historyRepository.GetById(id);


            /////找到子表，并删除子表的数据
            //int[] scoreArray = EducationinfoBLL.GetIdByMainId(id);
            //EducationinfoBLL.DeleteCollection(ref validationErrors, scoreArray);

            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<hr_pam_part> part = _partBLL.Bindpart(collection);
            return View(new GridModel(part));
        }

        #endregion
    }
}
