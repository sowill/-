﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Data;
using CHAIN.DAL;
namespace CHAIN.DAL
{
    /// <summary>
    /// 人员
    /// </summary>
    public class SysPersonRepository : BaseRepository<SysPerson>, IDisposable
    {
        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="SysEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param> 
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public IQueryable<SysPerson> DaoChuData(SysEntities db, string order, string sort, string search, params object[] listQuery)
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
                  
                    
                    
                    if (queryDic.ContainsKey("SysRole")&& !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key == "SysRole")
                    {//查询多对多关系的列名
                        where += "EXISTS(select p from it.SysRole as p where p.Id='" + item.Value + "')";
                        continue;
                    }
                    if (queryDic.ContainsKey("FileUploader")&& !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key == "FileUploader")
                    {//查询多对多关系的列名
                        where += "EXISTS(select p from it.FileUploader as p where p.Id='" + item.Value + "')";
                        continue;
                    }
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
            return db.SysPerson
                     .Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it." + sort.GetString() + " " + order.GetString())
                     .AsQueryable();   

        }
        /// <summary>
        /// 通过主键id，获取人员---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>人员</returns>
        public SysPerson GetById(string id)
        {
            using (SysEntities db = new SysEntities())
            {
                return GetById(db, id);
            }                   
        }
        /// <summary>
        /// 通过主键id，获取人员---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>人员</returns>
        public SysPerson GetById(SysEntities db, string id)
        { 
                 return db.SysPerson.SingleOrDefault(s => s.Id == id); 
        }
        
        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysRole> GetRefSysRole(string id)
        {
            using (SysEntities db = new SysEntities())
            {
                return GetRefSysRole(db, id);
            }
        }
        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysRole> GetRefSysRole(SysEntities db, string id)
        {
                return from m in db.SysPerson
                       from f in m.SysRole
                       where m.Id == id
                       select f;

        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysRole> GetRefSysRole(SysEntities db)
        {
            return from m in db.SysPerson
                   from f in m.SysRole
                   select f;
        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysRole> GetRefSysRole()
        {
            using (SysEntities db = new SysEntities())
            {
                return GetRefSysRole(db);
            }
        }

        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<FileUploader> GetRefFileUploader(string id)
        {
            using (SysEntities db = new SysEntities())
            {
                return GetRefFileUploader(db, id);
            }
        }
        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<FileUploader> GetRefFileUploader(SysEntities db, string id)
        {
                return from m in db.SysPerson
                       from f in m.FileUploader
                       where m.Id == id
                       select f;

        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<FileUploader> GetRefFileUploader(SysEntities db)
        {
            return from m in db.SysPerson
                   from f in m.FileUploader
                   select f;
        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<FileUploader> GetRefFileUploader()
        {
            using (SysEntities db = new SysEntities())
            {
                return GetRefFileUploader(db);
            }
        }

       
        /// <summary>
        /// 确定删除一个对象，调用Save方法
        /// </summary>
        /// <param name="id">一条数据的主键</param>
        /// <returns></returns>    
        public int Delete(string id)
        {
            using (SysEntities db = new SysEntities())
            {
                this.Delete(db, id);
                return Save(db);
            }
        }
        /// <summary>
        /// 删除一个人员
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条人员的主键</param>
        public void Delete(SysEntities db, string id)
        {
            SysPerson deleteItem = GetById(db, id);
            if (deleteItem != null)
            { 
                db.SysPerson.DeleteObject(deleteItem);
            }
        }
        /// <summary>
        /// 删除对象集合
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="deleteCollection">主键的集合</param>
        public void Delete(SysEntities db, string[] deleteCollection)
        {
            //数据库设置级联关系，自动删除子表的内容   
            IQueryable<SysPerson> collection = from f in db.SysPerson
                    where deleteCollection.Contains(f.Id)
                    select f;
            foreach (var deleteItem in collection)
            {
                db.SysPerson.DeleteObject(deleteItem);
            }
        }

        public void Dispose()
        {
        }
    }
}

