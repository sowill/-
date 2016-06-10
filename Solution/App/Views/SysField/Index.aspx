<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Index.Master" Inherits="System.Web.Mvc.ViewPage<CHAIN.DAL.SysField>" %>

<%@ Import Namespace="Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    数据字典
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(function () {

                $('#flexigridData').treegrid({
                    title: '数据字典',
                     iconCls: 'icon-site',
//                    url: '../SysField/GetAllMetadata',
                    idField: 'Id',
                    treeField: 'MyTexts',
                    rownumbers: true,
                    toolbar: [
                    {
                        text: '详细',
                        iconCls: 'icon-search',
                        handler: function () {
                            return getView();
                        }
                    }, {
                        text: '创建',
                        iconCls: 'icon-add',
                        handler: function () {
                            return flexiCreate();
                        }
                    },  {
                        text: '删除',
                        iconCls: 'icon-remove',
                        handler: function () {
                            return flexiDelete();
                        }
                    }, {
                        text: '修改',
                        iconCls: 'icon-edit',
                        handler: function () {
                            return flexiModify();
                        }
                    }, {
                        text: '选择',
                        iconCls: 'icon-ok',
                        handler: function () {
                            return flexiSelect();
                        }
                    }],
                  
                    columns: [[ 
                    	{ field: 'MyTexts', title: '名称', width: 115 }
                        
					,{ field: 'MyTables', title:  '<%: Html.LabelFor(model => model.MyTables) %>', width: 96 }
					,{ field: 'MyColums', title:  '<%: Html.LabelFor(model => model.MyColums) %>', width: 96 }
					,{ field: 'Sort', title:  '<%: Html.LabelFor(model => model.Sort) %>', width: 96 }
					,{ field: 'Remark', title:  '<%: Html.LabelFor(model => model.Remark) %>', width: 96 }
					,{ field: 'CreateTime', title:  '<%: Html.LabelFor(model => model.CreateTime) %>', width: 96
                    , formatter: function (value, rec) {
                        if (value) {
                            return dateConvert(value);
                        } 
                    } 
}
					,{ field: 'CreatePerson', title:  '<%: Html.LabelFor(model => model.CreatePerson) %>', width: 96 }
					,{ field: 'UpdateTime', title:  '<%: Html.LabelFor(model => model.UpdateTime) %>', width: 96
                    , formatter: function (value, rec) {
                        if (value) {
                            return dateConvert(value);
                        } 
                    } 
}
					,{ field: 'UpdatePerson', title:  '<%: Html.LabelFor(model => model.UpdatePerson) %>', width: 96 }
				    ]]
                     ,
                    onBeforeLoad: function (row, param) {
                       
                        if (row) {
                            $(this).treegrid('options').url = '../SysField/GetAllMetadata/' + row.Id;
                        } else {
                            $(this).treegrid('options').url = '../SysField/GetAllMetadata';
                        }
                    }
                });
          
                var parent = window.dialogArguments; //获取父页面
                if (parent == "undefined" || parent == null) {
                    $(".l-btn.l-btn-plain:last").hide();
                } else {
                    $(".l-btn.l-btn-plain").hide();
                    $(".datagrid-btn-separator").hide();
                    $(".l-btn.l-btn-plain:last").show();
                }
         
         });

         function flexiSelect() {
            var node = $('#flexigridData').treegrid('getSelected');
            if (!node) {
                $.messager.alert('操作提示', '请选择一条数据!', 'warning');
               return false;
            }
            var arr = new Array(0);
            arr.push(node.Id);
            arr.push("^"); //主键列和显示列的分割符 ^ 
            arr.push(node.MyTexts);
            //主键列和显示列之间用 ^ 分割   每一项用 , 分割
            if (arr.length == 3) {//一条数据和多于一条
                returnParent(arr.join("&")); //每一项用 & 分割
            }
             return false;
        }
        //导航到查看详细的按钮
        function getView() {

            var arr = $('#flexigridData').treegrid('getSelected');

            if (arr) {
                window.location.href = "../SysField/Details/" + arr.Id;
               
            } else {
                $.messager.alert('操作提示', '请选择一条数据!', 'warning');
            }
            return false;
        }
        //导航到创建的按钮
        function flexiCreate() {

            window.location.href = "../SysField/Create";
            return false;
        }
        //导航到修改的按钮
        function flexiModify() {

            var arr = $('#flexigridData').treegrid('getSelected');

            if (arr) {
                window.location.href = "../SysField/Edit/" + arr.Id;
               
            } else {
                $.messager.alert('操作提示', '请选择一条数据!', 'warning');
            }
             return false;
        };
        //删除的按钮
        function flexiDelete() {
          
            var node = $('#flexigridData').treegrid('getSelected');
            if (!node) {
                $.messager.alert('操作提示', '请选择数据!', 'warning');

            } else {
                $.messager.confirm('操作提示', "确认删除这1项吗？", function (r) {
                    if (r) {
                        $.post("../SysField/Delete", { query: node.Id }, function (res) {
                            if (res == "OK") {
                                remove();
                                $.messager.alert('操作提示', '删除成功!', 'info');                  
                            }
                            else {
                                if (res == "") {
                                    $.messager.alert('操作提示', '删除失败!请查看该数据与其他模块下的信息的关联，或联系管理员。', 'info');
                                }
                                else {
                                    $.messager.alert('操作提示', res, 'info');
                                }
                            }             
                        }); 
                    }
                });
            }                  
            return false;
        };                
       
       
        function remove() {
            var node = $('#flexigridData').treegrid('getSelected');
            if (node) {
                $('#flexigridData').treegrid('remove', node.Id);
            }
        }
    </script>
</asp:Content>

