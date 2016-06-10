using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Data;
using System.Collections;
namespace CHAIN.DAL
{
    /// <summary>
    /// hr_srv_history
    /// </summary>
    public partial class hr_srv_historyRepository : BaseRepository<hr_srv_history>, IDisposable
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
        public IQueryable<hr_srv_history> DaoChuData(SysEntities db, string order, string sort, string search, params object[] listQuery)
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
                        where += "it." + item.Key.Remove(item.Key.IndexOf(End_String)) + " = '" + item.Value + "'";
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(DDL_String)) //需要查询的列名
                    {
                        where += "it." + item.Key.Remove(item.Key.IndexOf(DDL_String)) + " = '" + item.Value + "'";
                        continue;
                    }
                    where += "it." + item.Key + " like '%" + item.Value + "%'";
                }
            }
            return db.hr_srv_history
                     .Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it." + sort.GetString() + " " + order.GetString())
                     .AsQueryable();

        }
        /// <summary>
        /// 通过主键id，获取hr_srv_history---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>hr_srv_history</returns>
        public hr_srv_history GetById(int id)
        {
            using (SysEntities db = new SysEntities())
            {
                return GetById(db, id);
            }
        }
        /// <summary>
        /// 通过主键id，获取hr_srv_history---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>hr_srv_history</returns>
        public hr_srv_history GetById(SysEntities db, int id)
        {
            return db.hr_srv_history.SingleOrDefault(s => s.id == id);

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
        /// 删除一个hr_srv_history
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条hr_srv_history的主键</param>
        public void Delete(SysEntities db, int id)
        {
            hr_srv_history deleteItem = GetById(db, id);
            if (deleteItem != null)
            {
                db.hr_srv_history.DeleteObject(deleteItem);
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
            IQueryable<hr_srv_history> collection = from f in db.hr_srv_history
                                                    where deleteCollection.Contains(f.id)
                                                    select f;
            foreach (var deleteItem in collection)
            {
                db.hr_srv_history.DeleteObject(deleteItem);
            }
        }

        public void Dispose()
        {
        }
#region  出行一
        public IEnumerable<hr_srv_history> Bindhistory(string searchDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("0", "返回");
            ht.Add("1", "出行");
            ht.Add("-1", "");
            var templist = from b in db.hr_pam_employee.AsEnumerable()
                           join a in
                               (from a1 in db.hr_srv_history.AsEnumerable() where a1.chuxing_date.CompareTo(Convert.ToDateTime(searchDate)) == 0 select a1)
                           on b.id equals a.employeeid into temp_b
                           from temp_b2 in temp_b.DefaultIfEmpty()
                            select new
                           {
                               id = (temp_b2 == null ? 0 : temp_b2.id),
                               employeeid =  b.id,
                               employeename =  b.employeename,
                               chuxing = (temp_b2 == null ? -1 : temp_b2.chuxing),
                               chuxing1 = (ht[(temp_b2==null ? -1 : temp_b2.chuxing).ToString()]),
                               chuxing_date = (temp_b2 == null ? Convert.ToDateTime(searchDate) : temp_b2.chuxing_date),
                               
                               memo = (temp_b2 == null ? "" : temp_b2.memo)
                           };
            IEnumerable<hr_srv_history> list = from a in templist

                                               select new hr_srv_history
                                               {
                                                   id = a.id,
                                                   employeeid=a.employeeid,
                                                   employeename = a.employeename,
                                                   chuxing = a.chuxing,
                                                   chuxing1 = a.chuxing1,
                                                   chuxing_date = a.chuxing_date,
                                                   
                                                   memo = a.memo
                                               };
            return list;
        }
#endregion
        #region 出行二
        public IEnumerable<hr_srv_history> Bindhistory1()
        {
            //Hashtable ht = new Hashtable();
            //ht.Add("0", "返回");
            //ht.Add("1", "出行");
            //ht.Add("-1", "");
            //var templist = from b in db.hr_pam_employee.AsEnumerable()
            //               join a in
            //                   (from a1 in db.hr_srv_history.AsEnumerable() select a1)
            //               on b.id equals a.employeeid into temp_b
            //               from temp_b2 in temp_b.DefaultIfEmpty()
            //               select new
            //               {
            //                   id = (temp_b2 == null ? 0 : temp_b2.id),
            //                   employeeid = b.id,
            //                   employeename = b.employeename,
            //                   chuxing = (temp_b2 == null ? -1 : temp_b2.chuxing),
            //                   chuxing1 = (ht[(temp_b2 == null ? -1 : temp_b2.chuxing).ToString()]),
            //                   chuxing_date = (temp_b2 == null ? Convert.ToDateTime(DateTime.Now.ToShortDateString()) : temp_b2.chuxing_date),
            //                   fanhui_date = (temp_b2 == null ? Convert.ToDateTime(DateTime.Now.ToShortDateString()):temp_b2.fanhui_date),
            //                   memo = (temp_b2 == null ? "" : temp_b2.memo)
            //               };
            //IEnumerable<hr_srv_history> list = from a in templist

            //                                   select new hr_srv_history
            //                                   {
            //                                       id = a.id,
            //                                       employeeid = a.employeeid,
            //                                       employeename = a.employeename,
            //                                       chuxing = a.chuxing,
            //                                       chuxing1 = a.chuxing1,
            //                                       chuxing_date = a.chuxing_date,
            //                                       fanhui_date=a.fanhui_date,
            //                                       memo = a.memo
            //                                   };
            //return list;


            Hashtable ht = new Hashtable();
            ht.Add("0", "返回");
            ht.Add("1", "出行");
            ht.Add("-1", "");
            var templist = from b in db.hr_pam_employee.AsEnumerable()
                           join a in
                               (from a1 in db.hr_srv_history.AsEnumerable() select a1)
                           on b.id equals a.employeeid
                           select new
                           {
                               id = (a == null ? 0 : a.id),
                               employeeid = b.id,
                               employeename = b.employeename,
                               chuxing = (a == null ? -1 : a.chuxing),
                               chuxing1 = (ht[(a == null ? -1 : a.chuxing).ToString()]),
                               chuxing_date = (a == null ? Convert.ToDateTime(DateTime.Now.ToShortDateString()) : a.chuxing_date),
                               fanhui_date = (a == null ? Convert.ToDateTime(DateTime.Now.ToShortDateString()) : a.fanhui_date),
                               memo = (a == null ? "" : a.memo)
                           };
            IEnumerable<hr_srv_history> list = from a in templist

                                               select new hr_srv_history
                                               {
                                                   id = a.id,
                                                   employeeid = a.employeeid,
                                                   employeename = a.employeename,
                                                   chuxing = a.chuxing,
                                                   chuxing1 = a.chuxing1,
                                                   chuxing_date = a.chuxing_date,
                                                   fanhui_date = a.fanhui_date,
                                                   memo = a.memo
                                               };
            return list;


            //Hashtable ht = new Hashtable();
            //ht.Add("0", "返回");
            //ht.Add("1", "出行");
            //ht.Add("-1", "");
            //var templist = from a in db.hr_srv_history.AsEnumerable()
            //               select new
            //               {
            //                   id = a.id,
            //                   employeeid = a.employeeid,
            //                   employeename = a.employeename,
            //                   chuxing = a.chuxing,
            //                   chuxing1 = a.chuxing1,
            //                   chuxing_date = a.chuxing_date,
            //                   fanhui_date = a.fanhui_date,
            //                   memo = a.memo
            //               };
            //IEnumerable<hr_srv_history> list = from a in templist
            //                                   select new hr_srv_history
            //                                   {
            //                                       id = a.id,
            //                                       employeeid = a.employeeid,
            //                                       employeename = a.employeename,
            //                                       chuxing = a.chuxing,
            //                                       chuxing1 = a.chuxing1,
            //                                       chuxing_date = a.chuxing_date,
            //                                       fanhui_date = a.fanhui_date,
            //                                       memo = a.memo
            //                                   };
            //return list;



        }
        #endregion
    }
}

