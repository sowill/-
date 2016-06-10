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
    public class part1Controller : Controller
    {
        //
        // GET: /part1/
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
        # region 员工表操作
        [GridAction]
        public ActionResult _Indexemployees()
        {

            //实例化一个集合
            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            //调用BLL层中的Bindpart1方法，把值赋给part
            IEnumerable<CHAIN.DAL.hr_pam_part> part = _partBLL.Bindpart1(collection);
            //把part中的所有的值返回到view层中
            return View(new GridModel(part));
        }
        [GridAction]
        [HttpPost]
        public ActionResult _Insert_Editemployees(hr_pam_part item)
        {
            //实例化hr_pam_part
            hr_pam_part partDAL = new hr_pam_part();

            //判断尝试更新Model是否可以成功
            if (TryUpdateModel(partDAL))
            {
                if (item.id == 0)
                {
                    //调用Create方法，把数据添加到数据库中
                    _partBLL.Create(ref validationErrors, partDAL);
                }
                else
                {
                    //调用Edit方法，把更新后的数据添加到数据库中
                    _partBLL.Edit(ref validationErrors, partDAL);
                }
            }

            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<hr_pam_part> part = _partBLL.Bindpart1(collection);
            return View(new GridModel(part));
        }
        [GridAction]
        [HttpPost]
        public ActionResult _Deleteemployees(int id)
        {
            //增加一个id，删除一条数据
            _partBLL.Delete(ref validationErrors, id);
           
            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<hr_pam_part> part = _partBLL.Bindpart1(collection);
            return View(new GridModel(part));
        }
        #endregion

        #region 部门表操作
        [GridAction]
        public ActionResult _Indexdepartments(int eid)
        { 

            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<CHAIN.DAL.hr_pam_part> part = _partBLL.Bindpart2(eid);
            return View(new GridModel(part));

        }

        [GridAction]
        [HttpPost]
        public ActionResult _Insert_Editdepartments(int eid, hr_pam_part item)
        {
            
            hr_pam_part partDAL = new hr_pam_part();
            //验证是否重复

            int departmentnameget = _partRepository.Getdepartmentname(item.departmentid,eid);
            if (departmentnameget > 0)
            {

                Response.StatusCode = 403;
                return Content(string.Join("提示", "部门名称不能相同~"));
            }
            else
            {

                //更新数据
                if (TryUpdateModel(partDAL))
                {
                    partDAL.employeeid = eid;
                    if (item.id == 0)
                    {
                        _partBLL.Create(ref validationErrors, partDAL);
                    }
                    else
                    {
                        _partBLL.Edit(ref validationErrors, partDAL);
                    }
                }
            }
            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<hr_pam_part> part = _partBLL.Bindpart2(eid);
            
            return View(new GridModel(part));
        }
        [GridAction]
        [HttpPost]
        public ActionResult _Deletedepartments(int eid,int id)
        {
            
            ///更加id删除某条数据
            _partBLL.Delete(ref validationErrors, id);
           
            NameValueCollection collection = new NameValueCollection(Request.QueryString);
            IEnumerable<hr_pam_part> part = _partBLL.Bindpart2(eid);
            return View(new GridModel(part));
        }
        #endregion

    }
}
