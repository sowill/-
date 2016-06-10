using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHAIN.DAL
{
    public class AccountRepository
    {
        /// <summary>
        /// 登录时候的验证
        /// </summary>
        /// <param name="personName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>用户信息</returns>
        public static SysPerson ValidatePerson(string personName, string password)
        {
            if (String.IsNullOrEmpty(personName) || String.IsNullOrEmpty(password))
                return null;

            //获取用户信息
            using (SysEntities db = new SysEntities())
            {                 
                return (from p in db.SysPerson
                        where p.Name == personName
                        && p.Password == password 
                        && p.State=="开启"
                        select p).FirstOrDefault();
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="personName">用户名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>修改密码是否成功</returns>
        public static bool ChangePassword(string personName, string oldPassword, string newPassword)
        {
            using (SysEntities db = new SysEntities())
            {
                var person = db.SysPerson.FirstOrDefault(p => (p.Name == personName) && (p.Password == oldPassword));
                person.Password = newPassword;
                person.SurePassword = newPassword;
                db.SaveChanges();
                return true;
            }     
        }
    }
}

