﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Index.Master" Inherits="System.Web.Mvc.ViewPage<CHAIN.DAL.SysException>" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="CHAIN.App.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    异常处理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divQuery" title="查询列表" class="easyui-dialog" closed="true" modal="false"
        iconcls="icon-search">
         
            <div class="input">
                <div class="editor-label-search">
                    <%: Html.LabelFor(model => model.Message) %>
                </div>
                <div class="editor-field-search">
                    <input type='text' id='Message'/>
                </div>
            </div> 
            <br style="clear: both;" />
            <div class="editor-label-search">
                <%: Html.LabelFor(model => model.CreateTime) %>
            </div>
            <div class="editor-field-to">
                <input type="text" id="CreateTimeStart_Time" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'CreateTimeEnd_Time\');}'})"  />
                到
                <input type="text" id="CreateTimeEnd_Time" onclick="WdatePicker({minDate:'#F{$dp.$D(\'CreateTimeStart_Time\');}'})" />
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    
                               <script src="<%: Url.Content("~/Res/My97DatePicker/WdatePicker.js") %>" type="text/javascript"></script>
                                 
    <script type="text/javascript" language="javascript">
        $(function () {

            $('#flexigridData').datagrid({
                title: '异常处理', //列表的标题
                iconCls: 'icon-site',
                width: 'auto',
                height: 'auto',
                nowrap: false,
                striped: true,
                collapsible: true,
                url: 'SysException/GetData', //获取数据的url
                sortName: 'Id',
                sortOrder: 'desc',
                idField: 'Id',

                toolbar: [
                 {
                     text: '查询',
                     iconCls: 'icon-search',
                     handler: function () {
                         flexiQuery();
                     }
                 }],

                columns: [[
                   
                    
					{ field: 'LeiXing', title: '<%: Html.LabelFor(model => model.LeiXing) %>', width: 169 }
					,{ field: 'Message', title:  '<%: Html.LabelFor(model => model.Message) %>', width: 169 }
					,{ field: 'Result', title:  '<%: Html.LabelFor(model => model.Result) %>', width: 169 }
					,{ field: 'Remark', title:  '<%: Html.LabelFor(model => model.Remark) %>', width: 169 }
					,{ field: 'State', title:  '<%: Html.LabelFor(model => model.State) %>', width: 169 }
					,{ field: 'CreateTime', title:  '<%: Html.LabelFor(model => model.CreateTime) %>', width: 169
                    , formatter: function (value, rec) {
                        if (value) {
                            return dateConvert(value);
                        } 
                    } 
}
					,{ field: 'CreatePerson', title:  '<%: Html.LabelFor(model => model.CreatePerson) %>', width: 169 }
                ]],
                pagination: true,
                rownumbers: true

            });

            //如果列表页出现在弹出框中，则只显示查询和选择按钮 
            var parent = window.dialogArguments; //获取父页面
            if (parent == "undefined" || parent == null) {
                //异步获取按钮
                $.getJSON("../Home/GetToolbar", { id: "SysException", action: "Index" }, function (data) {
                    if (data == null) {
                        return;
                    }              
                    $('#flexigridData').datagrid("addToolbarItem", data);

                });

            } else {
                //添加选择按钮
                $('#flexigridData').datagrid("addToolbarItem", [{ "text": "选择", "iconCls": "icon-ok", handler: function () { flexiSelect(); } }]);
            }

        });

        //“查询”按钮，弹出查询框
        function flexiQuery() {
            $('#divQuery').dialog({          
                buttons: [{
                    text: '查询',
                    iconCls: 'icon-ok',
                    handler: function () {
                       //将查询条件按照分隔符拼接成字符串
                        var search = "";
                        $('#divQuery').find(":text,:selected,select,textarea,:hidden,:checked,:password").each(function () {
                            search = search + this.id + "&" + this.value + "^";
                        });
                        //执行查询                        
                        $('#flexigridData').datagrid('reload', { search: search });
                    }
                },
                     {
                         text: '取消',
                         iconCls: 'icon-cancel',
                         handler: function () {
                             $('#divQuery').dialog("close");
                         }
                     }]
            });
            $('#divQuery').dialog("open");
        };

        //“选择”按钮，在其他（与此页面有关联）的页面中，此页面以弹出框的形式出现，选择页面中的数据
        function flexiSelect() {

            var rows = $('#flexigridData').datagrid('getSelections');
            if (rows.length == 0) {
                $.messager.alert('操作提示', '请选择数据!', 'warning');
                return false;
            }

            var arr = [];
            for (var i = 0; i < rows.length; i++) {
                arr.push(rows[i].Id);
            }
            arr.push("^");
            for (var i = 0; i < rows.length; i++) {
                arr.push(rows[i].LeiXing);
            }
            //主键列和显示列之间用 ^ 分割   每一项用 , 分割
            if (arr.length > 0) {//一条数据和多于一条
                returnParent(arr.join("&")); //每一项用 & 分割
            }
        }
        //导航到查看详细的按钮
        function getView() {

            var arr = $('#flexigridData').datagrid('getSelections');

            if (arr.length == 1) {
                window.location.href = "../SysException/Details/" + arr[0].Id;
               
            } else {
                $.messager.alert('操作提示', '请选择一条数据!', 'warning');
            }
            return false;
        }
        //导航到创建的按钮
        function flexiCreate() {

            window.location.href = "../SysException/Create";
            return false;
        }
        //导航到修改的按钮
        function flexiModify() {

            var arr = $('#flexigridData').datagrid('getSelections');

            if (arr.length == 1) {
                window.location.href = "../SysException/Edit/" + arr[0].Id;

            } else {
                $.messager.alert('操作提示', '请选择一条数据!', 'warning');
            }
            return false;

        };
        //删除的按钮
        function flexiDelete() {

            var rows = $('#flexigridData').datagrid('getSelections');
            if (rows.length == 0) {
                $.messager.alert('操作提示', '请选择数据!', 'warning');
                return false;
            }

            var arr = [];
            for (var i = 0; i < rows.length; i++) {
                arr.push(rows[i].Id);
            }

            $.messager.confirm('操作提示', "确认删除这 " + arr.length + " 项吗？", function (r) {
                if (r) {
                    $.post("../SysException/Delete", { query: arr.join(",") }, function (res) {
                        if (res == "OK") {
                           //移除删除的数据
                            $("#flexigridData").datagrid("reload");
                            $("#flexigridData").datagrid("clearSelections");
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

        };

    </script>
</asp:Content>

