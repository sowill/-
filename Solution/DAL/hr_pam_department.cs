using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CHAIN.DAL
{
    [MetadataType(typeof(hr_pam_departmentMetadata))]//使用hr_pam_departmentMetadata对hr_pam_department进行数据验证
    public partial class hr_pam_department : IBaseEntity
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        
        #endregion

       
    }
    public class hr_pam_departmentMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "id", Order = 1)]
			public object id { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "部门名称", Order = 2)]
			[Required(ErrorMessage = "不能为空")]
			[StringLength(50, ErrorMessage = "长度不可超过50")]
			public object departmentname { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "部门描述", Order = 3)]
			[StringLength(50, ErrorMessage = "长度不可超过50")]
			public object departmentdetails { get; set; }

            [ScaffoldColumn(true)]
            [Display(Name = "部门人数", Order = 4)]
            [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
            public int? departmentnumbersum { get; set; }


    }


}

