using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHAIN.DAL;
namespace CHAIN.BLL
{
    public class HomeBLL : IDisposable
    {        
        /// <summary>
        /// 根据PersonId获取已经启用的菜单
        /// </summary>
        /// <param name="personId">人员的Id</param>
        /// <returns>菜单拼接的字符串</returns>
        public string GetMenuByPersonId(string personId)
        {
            HomeRepository home = new HomeRepository();
            return home.GetMenuByPersonId(personId);
        }

        public void Dispose()
        {

        }
    }
}

