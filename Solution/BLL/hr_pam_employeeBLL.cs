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
    /// hr_pam_employee 
    /// </summary>
    public partial class hr_pam_employeeBLL :  IBLL.Ihr_pam_employeeBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected SysEntities db;
        /// <summary>
        /// hr_pam_employee的数据库访问对象
        /// </summary>
        hr_pam_employeeRepository repository = new hr_pam_employeeRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public hr_pam_employeeBLL()
        {
            db = new SysEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public hr_pam_employeeBLL(SysEntities entities)
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
        public List<hr_pam_employee> GetByParam(int? id, int page, int rows, string order, string sort, string search, ref int total)
        {
            IQueryable<hr_pam_employee> queryData = repository.DaoChuData(db, order, sort, search);
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
        public List<hr_pam_employee> GetByParam(string id, string order, string sort, string search)
        {
            IQueryable<hr_pam_employee> queryData = repository.DaoChuData(db, order, sort, search);
            
            return queryData.ToList();
        }
        /// <summary>
        /// 创建一个hr_pam_employee
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个hr_pam_employee</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, hr_pam_employee entity)
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
        ///  创建hr_pam_employee集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">hr_pam_employee集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<hr_pam_employee> entitys)
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
        /// 删除一个hr_pam_employee
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一hr_pam_employee的主键</param>
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
        /// 删除hr_pam_employee集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">hr_pam_employee的集合</param>
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
        ///  创建hr_pam_employee集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">hr_pam_employee集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<hr_pam_employee> entitys)
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
        /// 编辑一个hr_pam_employee
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个hr_pam_employee</param>
        /// <returns></returns>
        public bool Edit(ref ValidationErrors validationErrors, hr_pam_employee entity)
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
      
        public List<hr_pam_employee> GetAll()
        {           
            return repository.GetAll(db).ToList();          
        }   
        
        /// <summary>
        /// 根据主键获取一个hr_pam_employee
        /// </summary>
        /// <param name="id">hr_pam_employee的主键</param>
        /// <returns>一个hr_pam_employee</returns>
        public hr_pam_employee GetById(int id)
        {           
            return repository.GetById(db, id);           
        }


        public void Dispose()
        {
           
        }
        public IEnumerable<hr_pam_employee> Bindemployee(NameValueCollection collection)
        {
            IEnumerable<hr_pam_employee> list = repository.Bindemployee();   //、调用Bindcontractinfo()方法，将查询到的所有的值赋给list。
            //string name_search = collection["employeename_search"];
            string name_search = (collection["employeename_search"] ?? "").Trim();
            string age_search = (collection["employeeage_search"]??"").Trim();
            string sex_search = collection["employeesex_search"];
            string study_search=collection["employeestudy_search"];
            string title_search = (collection["employeetitle_search"] ?? "").Trim();
            string salary_search = (collection["employeesalary_search"] ?? "").Trim();

            if (!string.IsNullOrWhiteSpace(name_search))
            {
                list = list.Where(a => (a.employeename ?? "").ToUpper().Contains(name_search.ToUpper()));
            }
            //if (!string.IsNullOrWhiteSpace(name_search))
            //{
            //    list = list.Where(a => (a.employeename).Contains(name_search));
            //}
             if (!string.IsNullOrWhiteSpace(age_search))    //、判断不是空白
             {
                 list = list.Where(a => a.employeeage == Convert.ToInt32(age_search));     //、"=>"代表获取字段的值
             }
             if (sex_search != "-1" && !string.IsNullOrWhiteSpace(sex_search))
            {
                list = list.Where(a => a.employeesex == Convert.ToInt32(sex_search));
            }
             if (study_search != "-1" && !string.IsNullOrWhiteSpace(study_search))
             {
                 list = list.Where(a => a.employeestudy == Convert.ToInt32(study_search));
             }
             if (!string.IsNullOrWhiteSpace(title_search))
             {
                 list = list.Where(a => (a.employeetitle ?? "").ToUpper().Contains(title_search.ToUpper()));
             }
             if (!string.IsNullOrWhiteSpace(salary_search))  
             {
                 list = list.Where(a => a.employeesalary == Convert.ToInt32(salary_search));   
             }
            return list;
        }

    }
}

