using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using CHAIN.DAL;
using Common;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace CHAIN.BLL
{
    /// <summary>
    /// hr_srv_history 
    /// </summary>
    public partial class hr_srv_historyBLL :  IBLL.Ihr_srv_historyBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected SysEntities db;
        /// <summary>
        /// hr_srv_history的数据库访问对象
        /// </summary>
        hr_srv_historyRepository repository = new hr_srv_historyRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public hr_srv_historyBLL()
        {
            db = new SysEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public hr_srv_historyBLL(SysEntities entities)
        {
            db = entities;
        }
        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        public List<hr_srv_history> GetByParam(int? id, int page, int rows, string order, string sort, string search, ref int total)
        {
            IQueryable<hr_srv_history> queryData = repository.DaoChuData(db, order, sort, search);
            total = queryData.Count();
            if (total > 0)
            {
                if (page <= 1)
                {
                    queryData = queryData.Take(rows);
                }
                else
                {
                    queryData = queryData.Skip((page - 1) * rows).Take(rows);
                }
                 
            }
            return queryData.ToList();
        }
        /// <summary>
        /// 查询的数据 /*在6.0版本中 新增*/
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        public List<hr_srv_history> GetByParam(string id, string order, string sort, string search)
        {
            IQueryable<hr_srv_history> queryData = repository.DaoChuData(db, order, sort, search);
            
            return queryData.ToList();
        }
        /// <summary>
        /// 创建一个hr_srv_history
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个hr_srv_history</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, hr_srv_history entity)
        {
            try
            {
                repository.Create(entity);
                return true;
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);                
            }
            return false;
        }
        /// <summary>
        ///  创建hr_srv_history集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">hr_srv_history集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<hr_srv_history> entitys)
        {
            try
            {
                if (entitys != null)
                {
                    int count = entitys.Count();
                    if (count == 1)
                    {
                        return this.Create(ref validationErrors, entitys.FirstOrDefault());
                    }
                    else if (count > 1)
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        { 
                            repository.Create(db, entitys);
                            if (count == repository.Save(db))
                            {
                                transactionScope.Complete();
                                return true;
                            }
                            else
                            {
                                Transaction.Current.Rollback();
                            }                          
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);                
            }
            return false;
        }
        /// <summary>
        /// 删除一个hr_srv_history
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一hr_srv_history的主键</param>
        /// <returns></returns>  
        public bool Delete(ref ValidationErrors validationErrors, int id)
        {
            try
            {
                return repository.Delete(id) == 1;
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);                
            }
            return false;
        }
        /// <summary>
        /// 删除hr_srv_history集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">hr_srv_history的集合</param>
        /// <returns></returns>    
        public bool DeleteCollection(ref ValidationErrors validationErrors, int[] deleteCollection)
        {
            try
            {
                if (deleteCollection != null)
                { 
                        using (TransactionScope transactionScope = new TransactionScope())
                        { 
                            repository.Delete(db, deleteCollection);
                            if (deleteCollection.Length == repository.Save(db))
                            {
                                transactionScope.Complete();
                                return true;
                            }
                            else
                            {
                                Transaction.Current.Rollback();
                            }
                        }
                    }
             
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);                
            }
            return false;
        }
        /// <summary>
        ///  创建hr_srv_history集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">hr_srv_history集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<hr_srv_history> entitys)
        {
            try
            {
                if (entitys != null)
                {
                    int count = entitys.Count();
                    if (count == 1)
                    {
                        return this.Edit(ref validationErrors, entitys.FirstOrDefault());
                    }
                    else if (count > 1)
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        { 
                            repository.Edit(db, entitys);
                            if (count == repository.Save(db))
                            {
                                transactionScope.Complete();
                                return true;
                            }
                            else
                            {
                                Transaction.Current.Rollback();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);                
            }
            return false;
        }
         /// <summary>
        /// 编辑一个hr_srv_history
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个hr_srv_history</param>
        /// <returns></returns>
        public bool Edit(ref ValidationErrors validationErrors, hr_srv_history entity)
        {
            try
            { 
                repository.Edit(db, entity);
                repository.Save(db);
                return true;
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);                
            }
            return false;
        }
      
        public List<hr_srv_history> GetAll()
        {           
            return repository.GetAll(db).ToList();          
        }   
        
        /// <summary>
        /// 根据主键获取一个hr_srv_history
        /// </summary>
        /// <param name="id">hr_srv_history的主键</param>
        /// <returns>一个hr_srv_history</returns>
        public hr_srv_history GetById(int id)
        {           
            return repository.GetById(db, id);           
        }


        public void Dispose()
        {
           
        }
        #region 出行一
        public IEnumerable<hr_srv_history> Bindhistory(NameValueCollection collection)
        {
            
            string search_name = (collection["employeename_search"]??"").Trim();
            string search_chuxing = collection["chuxing_search"];
            string search_chuxing_date = collection["chuxing_date_search"];
            
            
            //string search_chuxing_date1 = collection["chuxing_date_search1"];
            //string search_chuxing_date2 = collection["chuxing_date_search2"];

            IEnumerable<hr_srv_history> list = repository.Bindhistory(search_chuxing_date);   //、调用Bindcontractinfo()方法，将查询到的所有的值赋给list。

            if (!string.IsNullOrWhiteSpace(search_name))
            {
                list = list.Where(a => (a.employeename??"").ToUpper().ToString().Contains(search_name.ToUpper()));
            }
            if (search_chuxing != "-1" &&  !string.IsNullOrWhiteSpace(search_chuxing))    //、判断不是空白
            {
                list = list.Where(a => a.chuxing == Convert.ToInt32(search_chuxing));     //、"=>"代表获取字段的值
            }
            
            return list;
        }
        #endregion 

        #region 出行二
        public IEnumerable<hr_srv_history> Bindhistory1(NameValueCollection collection)
        {

            string search_name = (collection["employeename_search"] ?? "").Trim();
            string search_chuxing = collection["chuxing_search"];
            string search_chuxing_date1 = collection["chuxing_date_search1"];
            string search_chuxing_date2 = collection["chuxing_date_search2"];

            IEnumerable<hr_srv_history> list = repository.Bindhistory1();   //、调用Bindcontractinfo()方法，将查询到的所有的值赋给list。

            if (!string.IsNullOrWhiteSpace(search_name))
            {
                list = list.Where(a => (a.employeename ?? "").ToUpper().ToString().Contains(search_name.ToUpper()));
            }
            if (search_chuxing != "-1" && !string.IsNullOrWhiteSpace(search_chuxing))    //、判断不是空白
            {
                list = list.Where(a => a.chuxing == Convert.ToInt32(search_chuxing));     //、"=>"代表获取字段的值
            }
            if (!string.IsNullOrWhiteSpace(search_chuxing_date1))
            { 
               list = list.Where(a => a.chuxing_date.CompareTo(Convert.ToDateTime(search_chuxing_date1)) >= 0);
             }
            if (!string.IsNullOrWhiteSpace(search_chuxing_date2))
            {
                list = list.Where(a => a.chuxing_date.CompareTo(Convert.ToDateTime(search_chuxing_date2)) <= 0);
            }


            return list;
        }
        #endregion

    }
}

