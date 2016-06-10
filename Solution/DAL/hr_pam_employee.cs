using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CHAIN.DAL
{
    [MetadataType(typeof(hr_pam_employeeMetadata))]//使用hr_pam_employeeMetadata对hr_pam_employee进行数据验证
    public partial class hr_pam_employee : IBaseEntity
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        [Display(Name = "职工性别", Order = 8)]
        public object employeesex1 { get; set; }

        [Display(Name = "职工学历", Order = 9)]
        public object employeestudy1 { get; set; }
        #endregion
       
    }
    public class hr_pam_employeeMetadata
    {
			[ScaffoldColumn(true)]
			[Display(Name = "id", Order = 1)]
			public object id { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "职工姓名", Order = 2)]
			[Required(ErrorMessage = "不能为空")]
			[StringLength(50, ErrorMessage = "长度不可超过50")]
			public object employeename { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "职工年龄", Order = 3)]
			[Required(ErrorMessage = "不能为空")]
			[Range(0,2147483646, ErrorMessage="数值超出范围")]
			public int? employeeage { get; set; }
           
        
            [ScaffoldColumn(true)]
            [Display(Name = "职工性别", Order = 4)]
            [Range(0, 2147483646, ErrorMessage = "请选择性别")]
            public int? employeesex { get; set; }

            [ScaffoldColumn(true)]
            [Display(Name = "职工学历", Order = 5)]
            [Range(0, 2147483646, ErrorMessage = "请选择学历")]
            public int? employeestudy { get; set; }

            [ScaffoldColumn(true)]
            [Display(Name = "职工职位", Order = 6)]
            [Required(ErrorMessage = "不能为空")]
            [StringLength(50, ErrorMessage = "长度不可超过50")]
            public object employeetitle { get; set; }

            [ScaffoldColumn(true)]
            [Display(Name = "职工工资", Order = 7)]
            [Required(ErrorMessage = "不能为空")]
            [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
            public int? employeesalary { get; set; }
    }


}

