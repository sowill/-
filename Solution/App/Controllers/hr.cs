using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CHAIN.DAL;
namespace CHAIN.App.Controllers
{
    public class hr
    {
        /// <summary>
        /// 绑定dropdownlist
        /// </summary>
        /// <param name="type">绑定类型</param>
        /// <returns>选项</returns>
        public static SelectList bindDpl(string type)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            switch (type)
            {
                case "职工性别":
                    lista.Add(new SelectListItem { Text = "---请选择---", Value = "-1" });
                    lista.Add(new SelectListItem { Text = "女", Value = "1" });
                    lista.Add(new SelectListItem { Text = "男", Value = "0" });
                    break;
                case "出行状态":
                    lista.Add(new SelectListItem { Text = "---请选择---", Value = "-1" });
                    lista.Add(new SelectListItem { Text = "返回", Value = "0" });
                    lista.Add(new SelectListItem { Text = "出行", Value = "1" });
                    break;
                    
                case "部门名称":
                    using (SysEntities db = new SysEntities())
                    {
                        List<hr_pam_department> list = (from o in db.hr_pam_department.AsEnumerable()
                                   select new hr_pam_department { id=o.id,  departmentname=o.departmentname }).ToList();
                        hr_pam_department department = new hr_pam_department();
                        department.id = -1;
                        department.departmentname = "---请选择---";
                        list.Add(department);
                        var list2 = list.OrderBy(a => a.id);
                        return new SelectList(list2.ToList(), "id", "departmentname");
                    }
                case "职工姓名":
                    using (SysEntities db = new SysEntities())
                    {
                        List<hr_pam_employee> list = (from o in db.hr_pam_employee.AsEnumerable()
                                                        select new hr_pam_employee { id = o.id, employeename = o.employeename }).ToList();
                        hr_pam_employee employee = new hr_pam_employee();
                        employee.id = -1;
                        employee.employeename = "---请选择---";
                        list.Add(employee);
                        var list2 = list.OrderBy(a => a.id);
                        return new SelectList(list2.ToList(), "id", "employeename");
                    }

                case "职工学历":
                    lista.Add(new SelectListItem { Text = "---请选择---", Value = "-1" });
                    lista.Add(new SelectListItem { Text = "本科", Value = "1" });
                    lista.Add(new SelectListItem { Text = "大专", Value = "0" });
                    lista.Add(new SelectListItem { Text = "专科", Value = "2" });
                    break;


            }
            SelectList items = new SelectList(lista, "Value", "Text");
            return items;
        }
    }
}