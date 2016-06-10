using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CHAIN.DAL
{
    [MetadataType(typeof(hr_pam_partMetadata))]//使用hr_pam_partMetadata对hr_pam_part进行数据验证
    public partial class hr_pam_part : IBaseEntity
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        [Display(Name = "职工姓名", Order = 4)]
        public string employeename { get; set; }

        [Display(Name = "部门名称", Order = 5)]
        public string departmentname { get; set; }
        #endregion

    }
    public class hr_pam_partMetadata
    {
            [ScaffoldColumn(false)]
            [Display(Name = "id", Order = 1)]
            public object id { get; set; }

            [ScaffoldColumn(true)]
            [Display(Name = "employeeid", Order = 2)]
            public object employeeid { get; set; }

            [ScaffoldColumn(true)]
            [Display(Name = "departmentid", Order = 3)]
            public object departmentid { get; set; }


    }


}

