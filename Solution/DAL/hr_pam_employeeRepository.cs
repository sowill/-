using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Data;
using CHAIN.DAL;
using System.Collections;
namespace CHAIN.DAL
{
    /// <summary>
    /// hr_pam_employee
    /// </summary>
    public partial class hr_pam_employeeRepository : BaseRepository<hr_pam_employee>, IDisposable
    {
        ///<summary>
        ///实例化一个类
        ///</summary>
        SysEntities db = new SysEntities();


        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="SysEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public IQueryable<hr_pam_employee> DaoChuData(SysEntities db, string order, string sort, string search, params object[] listQuery)
        {
            string where = string.Empty;
            int flagWhere = 0;

            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            if (queryDic != null && queryDic.Count > 0)
            {
                foreach (var item in queryDic)
                {
                    if (flagWhere != 0)
                    {
                        where += " and ";
                    }
                    flagWhere++;
                    
                    
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(Start_Time)) //需要查询的列名
                     {
                         where += "it. " + item.Key.Remove(item.Key.IndexOf(Start_Time)) + " >=  CAST('" + item.Value + "' as   System.DateTime)";
                         continue;
                     }
                     if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(End_Time)) //需要查询的列名
                     {
                         where += "it." + item.Key.Remove(item.Key.IndexOf(End_Time)) + " <  CAST('" + Convert.ToDateTime(item.Value).AddDays(1) + "' as   System.DateTime)";
                         continue;
                     }
                     if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(Start_Int)) //需要查询的列名
                     {
                         where += "it." + item.Key.Remove(item.Key.IndexOf(Start_Int)) + " >= " + item.Value.GetInt();
                         continue;
                     }
                     if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(End_Int)) //需要查询的列名
                     {
                         where += "it." + item.Key.Remove(item.Key.IndexOf(End_Int)) + " <= " + item.Value.GetInt();
                         continue;
                     }

                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(End_String)) //需要查询的列名
                     {
                         where += "it." + item.Key.Remove(item.Key.IndexOf(End_String)) + " = '" + item.Value +"'";
                         continue;
                     }

                     if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(DDL_String)) //需要查询的列名
                     {
                         where += "it." + item.Key.Remove(item.Key.IndexOf(DDL_String)) + " = '" + item.Value +"'";
                         continue;
                     }
                    where += "it." + item.Key + " like '%" + item.Value + "%'";
                }
            }
            return db.hr_pam_employee
                     .Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it." + sort.GetString() + " " + order.GetString())
                     .AsQueryable(); 

        }
        /// <summary>
        /// 通过主键id，获取hr_pam_employee---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>hr_pam_employee</returns>
        public hr_pam_employee GetById(int id)
        {
            using (SysEntities db = new SysEntities())
            {
                return GetById(db, id);
            }                   
        }
        /// <summary>
        /// 通过主键id，获取hr_pam_employee---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>hr_pam_employee</returns>
        public hr_pam_employee GetById(SysEntities db, int id)
        { 
            return db.hr_pam_employee.SingleOrDefault(s => s.id == id);
        
        }
        /// <summary>
        /// 确定删除一个对象，调用Save方法
        /// </summary>
        /// <param name="id">一条数据的主键</param>
        /// <returns></returns>    
        public int Delete(int id)
        {
            using (SysEntities db = new SysEntities())
            {
                this.Delete(db, id);
                return Save(db);
            }
        }
 
        /// <summary>
        /// 删除一个hr_pam_employee
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条hr_pam_employee的主键</param>
        public void Delete(SysEntities db, int id)
        {
            hr_pam_employee deleteItem = GetById(db, id);
            if (deleteItem != null)
            { 
                db.hr_pam_employee.DeleteObject(deleteItem);
            }
        }
        /// <summary>
        /// 删除对象集合
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="deleteCollection">主键的集合</param>
        public void Delete(SysEntities db, int[] deleteCollection)
        {
            //数据库设置级联关系，自动删除子表的内容   
            IQueryable<hr_pam_employee> collection = from f in db.hr_pam_employee
                    where deleteCollection.Contains(f.id)
                    select f;
            foreach (var deleteItem in collection)
            {
                db.hr_pam_employee.DeleteObject(deleteItem);
            }
        }

        public void Dispose()
        {          
        }

        public IEnumerable<hr_pam_employee> Bindemployee()
        {
            Hashtable ht = new Hashtable();
            ht.Add("0", "男");
            ht.Add("1", "女");
            ht.Add("-1", "---请选择---");
            Hashtable ht1=new Hashtable();
            ht1.Add("0","大专");
            ht1.Add("1","本科");
            ht1.Add("2", "专科");
            ht1.Add("-1", "---请选择---");


            var templist = from a in db.hr_pam_employee.AsEnumerable()
                           select new
                           {
                               id = a.id,
                               employeename = a.employeename,
                               employeeage = a.employeeage,
                               employeesex = a.employeesex,
                               employeesex1 = (ht[a.employeesex.ToString()]),
                               employeestudy = a.employeestudy,
                               employeestudy1 = (ht1[a.employeestudy.ToString()]),
                               employeetitle = a.employeetitle,
                               employeesalary = a.employeesalary
                           };
            IEnumerable<hr_pam_employee> list = from a in templist
                                              select new hr_pam_employee
                                              {
                                                id=a.id,
                                                employeename=a.employeename,
                                                employeeage=a.employeeage,
                                                employeesex=a.employeesex,
                                                employeesex1 = a.employeesex1,
                                                employeestudy = a.employeestudy,
                                                employeestudy1 = a.employeestudy1,
                                                employeetitle = a.employeetitle,
                                                employeesalary = a.employeesalary
                                              };
            return list;
        }

    }
}

