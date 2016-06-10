using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CHAIN.DAL
{
    [MetadataType(typeof(hr_srv_historyMetadata))]//使用hr_srv_historyMetadata对hr_srv_history进行数据验证
    public partial class hr_srv_history : IBaseEntity
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        [Display(Name = "出行情况", Order = 7)]
        public object chuxing1 { get; set; }

        [Display(Name = "职工姓名", Order = 8)]
        public string employeename { get; set; }
        #endregion
    }
    public class hr_srv_historyMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "id", Order = 1)]
			public object id { get; set; }

            [ScaffoldColumn(true)]
            [Display(Name = "employeeid", Order = 2)]
            public object employeeid { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "出行情况", Order = 3)]
			[Required(ErrorMessage = "不能为空")]
			[Range(0,2147483646, ErrorMessage="请选择出行情况")]
			public int? chuxing { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "出行日期", Order = 4)]
			[Required(ErrorMessage = "不能为空")]
			[DataType(System.ComponentModel.DataAnnotations.DataType.DateTime,ErrorMessage="时间格式不正确")]
			[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
			public DateTime? chuxing_date { get; set; }

            [ScaffoldColumn(true)]
            [Display(Name = "返回日期", Order = 5)]
            [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime? fanhui_date { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "备注", Order = 6)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object memo { get; set; }


    }


}

