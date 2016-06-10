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
    public class employeeController : Controller
    {
        //
        // GET: /employee/
        hr_pam_employeeBLL _employeeBLL = new hr_pam_employeeBLL();
        ValidationErrors validationErrors = new ValidationErrors();
        hr_pam_employeeRepository _employeeRepository = new hr_pam_employeeRepository();
        SysEntities db = new SysEntities();

        public void BindDpl()
        {
             ViewBag.dplemployeesex = hr.bindDpl("职工性别");
             ViewBag.dplemployeestudy = hr.bindDpl("职工学历");
            
        }

        public ActionResult Index(FormCollection collection)
        {
            ViewData["employeename_search"] = collection["employeename_search"];
            ViewData["employeeage_search"] = collection["employeeage_search"];
            ViewData["employeesex_search"] = collection["employeesex_search"];
            ViewData["employeestudy_search"] = collection["employeestudy_search"];
            ViewData["employeetitle_search"] = collection["employeetitle_search"];
            ViewData["employeesaraly_search"] = collection["employeesaraly_search"];
            BindDpl();
            return View();
        }

        #region 对员工模块进行操作
        [GridAction]
        public ActionResult _Index()
        {

            NameValueCollection collection = new NameValueCollection(Request.QueryString);

            IEnumerable<CHAIN.DAL.hr_pam_employee> employee = _employeeBLL.Bindemployee(collection);
            return View(new GridModel(employee));
        }
        [GridAction]
        [HttpPost]
        public ActionResult _Insert_Edit(hr_pam_employee item)
        {
            hr_pam_employee employeeDAL = new hr_pam_employee();
            
            //更新数据
            if (TryUpdateModel(employeeDAL))
            {
                if (item.id == 0)
                {
                    _employeeBLL.Create(ref validationErrors, employeeDAL);
                }
                else
                {
                    _employeeBLL.Edit(ref validationErrors, employeeDAL);
                }            
             }   
           
            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<hr_pam_employee> employee = _employeeBLL.Bindemployee(collection);
            return View(new GridModel(employee));

        }

        [GridAction]
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            ///更加id删除某条数据
            _employeeBLL.Delete(ref validationErrors, id);
            hr_pam_employee employeeDAL = _employeeRepository.GetById(id);


            
            /////找到子表，并删除子表的数据
            //int[] scoreArray = EducationinfoBLL.GetIdByMainId(id);
            //EducationinfoBLL.DeleteCollection(ref validationErrors, scoreArray);

            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<hr_pam_employee> employee = _employeeBLL.Bindemployee(collection);
            return View(new GridModel(employee));
        }

        #endregion 
    }
}
