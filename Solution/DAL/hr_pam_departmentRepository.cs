using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Data;
using CHAIN.DAL;
namespace CHAIN.DAL
{
    /// <summary>
    /// hr_pam_department
    /// </summary>
    public partial class hr_pam_departmentRepository : BaseRepository<hr_pam_department>, IDisposable
    {
        /// <summary>
        /// 实例化Sysentities类
        /// </summary>
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
        public IQueryable<hr_pam_department> DaoChuData(SysEntities db, string order, string sort, string search, params object[] listQuery)
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
            return db.hr_pam_department
                     .Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it." + sort.GetString() + " " + order.GetString())
                     .AsQueryable(); 

        }
        /// <summary>
        /// 通过主键id，获取hr_pam_department---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>hr_pam_department</returns>
        public hr_pam_department GetById(int id)
        {
            using (SysEntities db = new SysEntities())
            {
                return GetById(db, id);
            }                   
        }
        /// <summary>
        /// 通过主键id，获取hr_pam_department---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>hr_pam_department</returns>
        public hr_pam_department GetById(SysEntities db, int id)
        { 
            return db.hr_pam_department.SingleOrDefault(s => s.id == id);
        
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
        /// 删除一个hr_pam_department
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条hr_pam_department的主键</param>
        public void Delete(SysEntities db, int id)
        {
            hr_pam_department deleteItem = GetById(db, id);
            if (deleteItem != null)
            { 
                db.hr_pam_department.DeleteObject(deleteItem);
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
            IQueryable<hr_pam_department> collection = from f in db.hr_pam_department
                    where deleteCollection.Contains(f.id)
                    select f;
            foreach (var deleteItem in collection)
            {
                db.hr_pam_department.DeleteObject(deleteItem);
            }
        }

        public void Dispose()
        { 
        }
        public IEnumerable<hr_pam_department> Binddepartment()
        {
            var templist =from a in db.hr_pam_department.AsEnumerable()

                          join b in db.hr_pam_part on a.id equals b.departmentid into temp_b
                          from temp_b1 in temp_b.DefaultIfEmpty()
                          group new  {a} by new {a.id,a.departmentnumbersum,a.departmentname,a.departmentdetails} into g
                                  select new
                                  {
                                      id = g.Key.id,
                                      departmentname = g.Key.departmentname,
                                      //departmentnumbersum=g.Sum(p=>p.departmentnumbersum),
                                      departmentdetails = g.Key.departmentdetails
                                  };


            IEnumerable<hr_pam_department> list = from a in templist
                select new hr_pam_department
                {
                    id=a.id,
                    departmentname=a.departmentname,
                    departmentdetails=a.departmentdetails
                };
            return list; 
        }

        public int GetName(string name,int id)
        {
            var list = from o in db.hr_pam_department.AsEnumerable()
                       where o.departmentname == name && o.id != id
                       select new hr_pam_department
                       {
                           id = o.id,
                           departmentname = o.departmentname,
                           departmentdetails = o.departmentdetails,
                       };
            return list.Count();
        }
    }
}

