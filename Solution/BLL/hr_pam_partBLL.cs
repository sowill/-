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
    /// hr_pam_part 
    /// </summary>
    public partial class hr_pam_partBLL :  IBLL.Ihr_pam_partBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected SysEntities db;
        /// <summary>
        /// hr_pam_part的数据库访问对象
        /// </summary>
        hr_pam_partRepository repository = new hr_pam_partRepository();
        private string search_deparrtmentname;
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public hr_pam_partBLL()
        {
            db = new SysEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public hr_pam_partBLL(SysEntities entities)
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
        public List<hr_pam_part> GetByParam(int? id, int page, int rows, string order, string sort, string search, ref int total)
        {
            IQueryable<hr_pam_part> queryData = repository.DaoChuData(db, order, sort, search);
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
        public List<hr_pam_part> GetByParam(string id, string order, string sort, string search)
        {
            IQueryable<hr_pam_part> queryData = repository.DaoChuData(db, order, sort, search);
            
            return queryData.ToList();
        }
        /// <summary>
        /// 创建一个hr_pam_part
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个hr_pam_part</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, hr_pam_part entity)
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
        ///  创建hr_pam_part集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">hr_pam_part集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<hr_pam_part> entitys)
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
        /// 删除一个hr_pam_part
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一hr_pam_part的主键</param>
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
        /// 删除hr_pam_part集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">hr_pam_part的集合</param>
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
        ///  创建hr_pam_part集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">hr_pam_part集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<hr_pam_part> entitys)
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
        /// 编辑一个hr_pam_part
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个hr_pam_part</param>
        /// <returns></returns>
        public bool Edit(ref ValidationErrors validationErrors, hr_pam_part entity)
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
      
        public List<hr_pam_part> GetAll()
        {           
            return repository.GetAll(db).ToList();          
        }   
        
        /// <summary>
        /// 根据主键获取一个hr_pam_part
        /// </summary>
        /// <param name="id">hr_pam_part的主键</param>
        /// <returns>一个hr_pam_part</returns>
        public hr_pam_part GetById(int id)
        {           
            return repository.GetById(db, id);           
        }


        public void Dispose()
        {
           
        }
        #region 一对一隶属关系
        public IEnumerable<hr_pam_part> Bindpart(NameValueCollection collection)
        {

            string search_employeename = (collection["employeename_search"] ?? "").Trim();
            string search_departmentname = (collection["departmentname_search"] ?? "").Trim();

            IEnumerable<hr_pam_part> list = repository.Bindpart((search_employeename??"").ToUpper());   //、调用Bindcontractinfo()方法，将查询到的所有的值赋给list。

            //if (!string.IsNullOrWhiteSpace(search_employeename))
            //{
            //    list = list.Where(a => (a.employeename ?? "").ToUpper().Contains(search_employeename.ToUpper()));
            //}
            if (search_departmentname != "-1" && !string.IsNullOrWhiteSpace(search_departmentname))
            {
                list = list.Where(a => a.departmentid == Convert.ToInt32(search_departmentname));
            }

            return list;
        }

        #endregion

        #region 多对多隶属关系
        public IEnumerable<hr_pam_part> Bindpart1(NameValueCollection collection)
        {
            //调用Bindpart1()方法，将查询到的所有的值赋给list
            string search_employeename = (collection["employeename_search"] ?? "").Trim();
            string search_departmentname = (collection["departmentname_search"] ?? "-1").Trim();
            
            //、调用Bindcontractinfo()方法，将查询到的所有的值赋给list。
            IEnumerable<hr_pam_part> list = repository.Bindpart1(Convert.ToInt32(search_departmentname));   
            

            if (!string.IsNullOrWhiteSpace(search_employeename))
            {
                list = list.Where(a => (a.employeename ?? "").ToUpper().Contains(search_employeename.ToUpper()));
            }
            //if (search_departmentname != "-1" && !string.IsNullOrWhiteSpace(search_departmentname))
            //{
            //    list = list.Where(a => a.departmentid == Convert.ToInt32(search_departmentname));
            //}
            return list;
        }

        public IEnumerable<hr_pam_part> Bindpart2(int eid)
        {
            //、调用Bindcontractinfo()方法，将查询到的所有的值赋给list。
            IEnumerable<hr_pam_part> list = repository.Bindpart2(eid);   
            return list;
        }

        #endregion

      

    }
}

