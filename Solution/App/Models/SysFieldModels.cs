﻿using System.Collections.Generic;
using System.Linq;
using CHAIN.DAL;
using System.Web.Mvc;
using CHAIN.BLL;
namespace App.Models
{
    public class SysFieldModels 
    {
	/// <summary>
        /// 获取字段，首选默认
        /// </summary>
        /// <returns></returns>
        public static SelectList GetSysField(string table, string colum, string parentMyTexts)
        {
            if (string.IsNullOrWhiteSpace(table) || string.IsNullOrWhiteSpace(colum) || string.IsNullOrWhiteSpace(parentMyTexts))
            {
                List<SelectList> sl = new List<SelectList>();
                return new SelectList(sl);
            }
            using (SysFieldHander baseDDL = new SysFieldHander())
            {
                return new SelectList(baseDDL.GetSysField(table, colum, parentMyTexts), "MyTexts", "MyTexts");
            }
        }
        /// <summary>
        /// 获取字段，首选默认，MyTexts做为value值
        /// </summary>
        /// <returns></returns>
        public static SelectList GetSysField(string table, string colum)
        {
            if (string.IsNullOrWhiteSpace(table) || string.IsNullOrWhiteSpace(colum))
            {
                List<SelectList> sl = new List<SelectList>();
                return new SelectList(sl);
            }
            using (SysFieldHander baseDDL = new SysFieldHander())
            {
                return new SelectList(baseDDL.GetSysField(table, colum), "MyTexts", "MyTexts");
            }
        }
        /// <summary>
        /// 获取字段，首选默认，Id做为value值
        /// </summary>
        /// <returns></returns>
        public static SelectList GetSysFieldById(string table, string colum)
        {
            if (string.IsNullOrWhiteSpace(table) || string.IsNullOrWhiteSpace(colum))
            {
                List<SelectList> sl = new List<SelectList>();
                return new SelectList(sl);
            }
            using (SysFieldHander baseDDL = new SysFieldHander())
            {
                return new SelectList(baseDDL.GetSysField(table, colum), "Id", "MyTexts");
            }
        }
        /// <summary>
        /// 根据主键id，获取数据字典的展示字段
        /// </summary>
        /// <param name="id">父亲节点的主键</param>
        /// <returns></returns>
        public static string GetMyTextsById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return string.Empty;
            }
            using (SysFieldHander baseDDL = new SysFieldHander())
            {
                return baseDDL.GetMyTextsById(id);
            }
        }
    }
}
