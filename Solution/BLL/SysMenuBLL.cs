﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using CHAIN.DAL;
using Common;

namespace CHAIN.BLL
{
    /// <summary>
    /// 模块 
    /// </summary>
    public class SysMenuBLL : IBLL.ISysMenuBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected SysEntities db;
        /// <summary>
        /// 模块的数据库访问对象
        /// </summary>
        SysMenuRepository repository = new SysMenuRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public SysMenuBLL()
        {
            db = new SysEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public SysMenuBLL(SysEntities entities)
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
        public List<SysMenu> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {

            
            IQueryable<SysMenu> queryData = repository.DaoChuData(db, order, sort, search);
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
                
                    foreach (var item in queryData)
                    {
                        if (item.ParentId != null && item.SysMenu2 != null)
                        { 
                                item.ParentIdOld = item.SysMenu2.Name.GetString();//                            
                        }                    
 
                        if (item.SysRole != null)
                        {
                            item.SysRoleId = string.Empty;
                            foreach (var it in item.SysRole)
                            {
                                item.SysRoleId += it.Name + ' ';
                            }                         
                        } 

                    }
 
            }
            return queryData.ToList();
        }
        
        /// <summary>
        /// 创建一个模块
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个模块</param>
        /// <returns></returns>
       public bool Create(ref ValidationErrors validationErrors, SysEntities db, SysMenu entity)
        {   
            int count = 1;
        
            foreach (string item in entity.SysRoleId.GetIdSort())
            {
                SysRole sys = new SysRole { Id = item };
                db.SysRole.Attach(sys);
                entity.SysRole.Add(sys);
                count++;
            }

            repository.Create(db, entity);
            if (count == repository.Save(db))
            {
                return true;
            }
            else
            {
                validationErrors.Add("创建出错了");
            }
            return false;
        }
        /// <summary>
        /// 创建一个模块
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个模块</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, SysMenu entity)
        {
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                { 
                    if (Create(ref validationErrors, db, entity))
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
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);                
            }
            return false;
        }
        /// <summary>
        ///  创建模块集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">模块集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<SysMenu> entitys)
        {
            try
            {
                if (entitys != null)
                {
                    int flag = 0, count = entitys.Count();
                    if (count > 0)
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        {
                            foreach (var entity in entitys)
                            {
                                if (Create(ref validationErrors, db, entity))
                                {
                                    flag++;
                                }
                                else
                                {
                                    Transaction.Current.Rollback();
                                    return false;
                                }
                            }
                            if (count == flag)
                            {
                                transactionScope.Complete();
                                return true;
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
        /// 删除一个模块
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一个模块的主键</param>
        /// <returns></returns>  
        public bool Delete(ref ValidationErrors validationErrors, string id)
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
        /// 删除模块集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">主键的模块</param>
        /// <returns></returns>    
        public bool DeleteCollection(ref ValidationErrors validationErrors, string[] deleteCollection)
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
        ///  创建模块集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">模块集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<SysMenu> entitys)
        {
            if (entitys != null)
            {
                try
                {
                    int flag = 0, count = entitys.Count();
                    if (count > 0)
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        {
                            foreach (var entity in entitys)
                            {
                                if (Edit(ref validationErrors, db, entity))
                                {
                                    flag++;
                                }
                                else
                                {
                                    Transaction.Current.Rollback();
                                    return false;
                                }
                            }
                            if (count == flag)
                            {
                                transactionScope.Complete();
                                return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    validationErrors.Add(ex.Message);
                    ExceptionsHander.WriteExceptions(ex);                
                }
            }
            return false;
        }
        /// <summary>
        /// 编辑一个模块
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">一个模块</param>
        /// <returns>是否编辑成功</returns>
       public bool Edit(ref ValidationErrors validationErrors, SysEntities db, SysMenu entity)
        {  /*                       
                           * 不操作 原有 现有
                           * 增加   原没 现有
                           * 删除   原有 现没
                           */
            if (entity == null)
            {
                return false;
            }
            int count = 1;
            SysMenu editEntity = repository.Edit(db, entity);
            
            List<string> addSysRoleId = new List<string>();
            List<string> deleteSysRoleId = new List<string>();
            DataOfDiffrent.GetDiffrent(entity.SysRoleId.GetIdSort(), entity.SysRoleIdOld.GetIdSort(), ref addSysRoleId, ref deleteSysRoleId);
            if (addSysRoleId != null && addSysRoleId.Count() > 0)
            {
                foreach (var item in addSysRoleId)
                {
                    SysRole sys = new SysRole { Id = item };
                    db.SysRole.Attach(sys);
                    editEntity.SysRole.Add(sys);
                    count++;
                }
            }
            if (deleteSysRoleId != null && deleteSysRoleId.Count() > 0)
            {
                List<SysRole> listEntity = new List<SysRole>();
                foreach (var item in deleteSysRoleId)
                {
                    SysRole sys = new SysRole { Id = item };
                    listEntity.Add(sys);
                    db.SysRole.Attach(sys);
                }
                foreach (SysRole item in listEntity)
                {
                    editEntity.SysRole.Remove(item);//查询数据库
                    count++;
                }
            } 

            if (count == repository.Save(db))
            {
                return true;
            }
            else
            {
                validationErrors.Add("编辑模块出错了");
            }
            return false;
        }
        /// <summary>
        /// 编辑一个模块
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个模块</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, SysMenu entity)
        {           
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                { 
                    if (Edit(ref validationErrors, db, entity))
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
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);                
            }
            return false;
        }
        public List<SysMenu> GetAll()
        {            
            return repository.GetAll(db).ToList();          
        }     
        
        /// <summary>
        /// 获取自连接树形列表数据
        /// </summary>
        /// <returns>自定义的树形结构</returns>
                   
        public IQueryable<SysMenu> GetAllMetadata(string id)
        { 
            if (id == null)
            {
                return db.SysMenu.Where(w => w.ParentId == null).AsQueryable();
            }
            else
            {
                return db.SysMenu.Where(w => w.ParentId == id).AsQueryable();
            }
        }

            
        /// <summary>
        /// 根据主键获取一个模块
        /// </summary>
        /// <param name="id">模块的主键</param>
        /// <returns>一个模块</returns>
        public SysMenu GetById(string id)
        {          
            return repository.GetById(db, id);           
        }
        
        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public List<SysRole> GetRefSysRole(string id)
        { 
            return repository.GetRefSysRole(db, id).ToList();
        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public List<SysRole> GetRefSysRole()
        { 
            return repository.GetRefSysRole(db).ToList();
        }

        
        public void Dispose()
        {
           
        }
    }
}

