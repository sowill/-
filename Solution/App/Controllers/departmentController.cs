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
    public class departmentController : Controller
    {
        //
        // GET: /department/
        hr_pam_departmentBLL _departmentBLL = new hr_pam_departmentBLL();
        ValidationErrors validationErrors = new ValidationErrors();
        hr_pam_departmentRepository _departmentRepository = new hr_pam_departmentRepository();
        SysEntities db = new SysEntities();

        public void BindDpl()
        {
            ViewBag.dpldepartmentname = hr.bindDpl("部门名称");
        }



        public ActionResult Index(FormCollection collection)
        {
            ViewData["departmentname_search"] = collection["departmentname_search"];
            BindDpl();
            return View();
        }

        #region 对部门模块操作
        [GridAction]
        public ActionResult _Index()
        {

            NameValueCollection collection = new NameValueCollection(Request.QueryString);

            IEnumerable<CHAIN.DAL.hr_pam_department> department = _departmentBLL.Binddepartment(collection);
            return View(new GridModel(department));
        }


        [GridAction]
        [HttpPost]
        public ActionResult _Insert_Edit(hr_pam_department item)
        {
            
            hr_pam_department departmentDAL = new hr_pam_department();
            //验证是否重复
            string name = item.departmentname;
            int nameget = _departmentRepository.GetName(name, item.id);
            if (nameget > 0)
            {

                Response.StatusCode = 403;
                return Content(string.Join("提示", "部门名称不能相同~"));
            }
            else
            {
                if (TryUpdateModel(departmentDAL))
                {

                    if (item.id == 0)
                    {
                        _departmentBLL.Create(ref validationErrors, departmentDAL);
                    }
                    else
                    {
                        _departmentBLL.Edit(ref validationErrors, departmentDAL);
                    }
                }
            }
            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<hr_pam_department> department = _departmentBLL.Binddepartment(collection);
            return View(new GridModel(department));
        }

        [GridAction]
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            ///更加id删除某条数据
            _departmentBLL.Delete(ref validationErrors, id);


            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<hr_pam_department> department = _departmentBLL.Binddepartment(collection);
            return View(new GridModel(department));
        }

        #endregion
    }
}
