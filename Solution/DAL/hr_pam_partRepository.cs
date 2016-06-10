using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Data;
using System.Collections;
using CHAIN.DAL;
using System.Web.Mvc;
namespace CHAIN.DAL
{
    
    /// hr_pam_part
    /// </summary>
    public partial class hr_pam_partRepository : BaseRepository<hr_pam_part>, IDisposable
    {
        ///<summary>
        ///新建一个类
        /// <summary>
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
        public IQueryable<hr_pam_part> DaoChuData(SysEntities db, string order, string sort, string search, params object[] listQuery)
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
            return db.hr_pam_part
                     .Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it." + sort.GetString() + " " + order.GetString())
                     .AsQueryable(); 

        }
        /// <summary>
        /// 通过主键id，获取hr_pam_part---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>hr_pam_part</returns>
        public hr_pam_part GetById(int id)
        {
            using (SysEntities db = new SysEntities())
            {
                return GetById(db, id);
            }                   
        }
        /// <summary>
        /// 通过主键id，获取hr_pam_part---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>hr_pam_part</returns>
        public hr_pam_part GetById(SysEntities db, int id)
        { 
            return db.hr_pam_part.SingleOrDefault(s => s.id == id);
        
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
        /// 删除一个hr_pam_part
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条hr_pam_part的主键</param>
        public void Delete(SysEntities db, int id)
        {
            hr_pam_part deleteItem = GetById(db, id);
            if (deleteItem != null)
            { 
                db.hr_pam_part.DeleteObject(deleteItem);
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
            IQueryable<hr_pam_part> collection = from f in db.hr_pam_part
                    where deleteCollection.Contains(f.id)
                    select f;
            foreach (var deleteItem in collection)
            {
                db.hr_pam_part.DeleteObject(deleteItem);
            }
        }

        public void Dispose()
        {          
        }
        #region 一对一隶属关系
        public IList<hr_pam_part> Bindpart(string searchName)
        {

            var templist = from a in db.hr_pam_employee.AsEnumerable()

                           join b in db.hr_pam_part on a.id equals b.employeeid into temp_b
                           from temp_b1 in temp_b.DefaultIfEmpty()

                           join c in db.hr_pam_department on (temp_b1 == null ? 0 : temp_b1.departmentid) equals c.id into temp_c
                           from temp_c1 in temp_c.DefaultIfEmpty()

                           where a.employeename.Contains(searchName)
                           select new
                           {
                               id = temp_b1 == null ? 0 : temp_b1.id,
                               employeeid = a.id,
                               employeename = a.employeename ?? "",
                               departmentid = temp_b1 == null ? 0 : temp_b1.departmentid,
                               departmentname = temp_c1 == null ? "" : temp_c1.departmentname
                           };
            IEnumerable<hr_pam_part> list = from a in templist
                                               select new hr_pam_part
                                               {
                                                   id=a.id,
                                                   employeeid=a.employeeid,
                                                   employeename=a.employeename,
                                                   departmentid=a.departmentid,
                                                   departmentname=a.departmentname
                                               };
            return list.ToList();
        }
        #endregion

        #region 多对多隶属关系

        public IEnumerable<hr_pam_part> Bindpart1(int departmentid)
        {
            if (departmentid > -1)
            {
                IEnumerable<hr_pam_part> list = from a in db.hr_pam_employee.AsEnumerable()

                                                join b in db.hr_pam_part on a.id equals b.employeeid into temp_b
                                                from temp_b1 in temp_b.DefaultIfEmpty()

                                                join c in db.hr_pam_department on (temp_b1 == null ? 0 : temp_b1.departmentid) equals c.id into temp_c
                                                from temp_c1 in temp_c.DefaultIfEmpty()
                                                where (temp_b1 == null ? 0 : temp_b1.departmentid) == departmentid
                                                group new { a } by new { a.id, a.employeename } into g

                                                //where g.Count() <= 1

                                                select new hr_pam_part
                               {
                                   //id = g.Key.id,
                                   employeeid = g.Key.id,
                                   employeename = g.Key.employeename,
                                   //departmentid = g.Key.departmentid
                                   //departmentname = g.Key.departmentname
                               };
                return list;
            }
            else {
                IEnumerable<hr_pam_part> list = from a in db.hr_pam_employee.AsEnumerable()

                                                join b in
                                                    (from b1 in db.hr_pam_part.AsEnumerable() select b1)
                                                on a.id equals b.employeeid into temp_b
                                                from temp_b1 in temp_b.DefaultIfEmpty()

                                                join c in db.hr_pam_department on (temp_b1 == null ? 0 : temp_b1.departmentid) equals c.id into temp_c
                                                from temp_c1 in temp_c.DefaultIfEmpty()
                                                //where (temp_b1 == null ? 0 : temp_b1.departmentid) == departmentid
                                                group new { a } by new { a.id, a.employeename } into g

                                                //where g.Count() <= 1

                                                select new hr_pam_part
                                                {
                                                    //id = g.Key.id,
                                                    employeeid = g.Key.id,
                                                    employeename = g.Key.employeename,
                                                    //departmentid = g.Key.departmentid
                                                    //departmentname = g.Key.departmentname
                                                };
                return list;
            }
        }

        public IList<hr_pam_part> Bindpart2(int employeeid)
        {

            var templist = from c in db.hr_pam_department.AsEnumerable()

                           join b in db.hr_pam_part on c.id equals b.departmentid

                           join a in db.hr_pam_employee on b.employeeid equals a.id

                           where b.employeeid == employeeid

                           select new
                           {
                               id =  b.id,
                               employeeid =a.id,
                               employeename=a.employeename,
                               departmentid = c.id,
                               departmentname = c.departmentname
                           };
            IEnumerable<hr_pam_part> list = from a in templist
                                            select new hr_pam_part
                                            {
                                                id = a.id,
                                                employeeid = a.employeeid,
                                                employeename=a.employeename,
                                                departmentid = a.departmentid,
                                                departmentname = a.departmentname
                                            };
            return list.ToList();
        }

        public int Getdepartmentname(int departmentid,int employeeid)
        {
            var list = from c in db.hr_pam_department.AsEnumerable()

                       join b in db.hr_pam_part on c.id equals b.departmentid

                       join a in db.hr_pam_employee on b.employeeid equals a.id

                       where  b.departmentid==departmentid && b.employeeid==employeeid 
                       select new hr_pam_part
                       {
                           id = b.id,
                           employeeid = b.employeeid,
                           employeename = a.employeename,
                           departmentid = b.departmentid,
                           departmentname = c.departmentname
                       };
            return list.Count();

        }
        #endregion

     
    }
}

