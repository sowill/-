<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Create.Master" Inherits="System.Web.Mvc.ViewPage<CHAIN.DAL.SysPerson>" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="CHAIN.App.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CurentPlace" runat="server">
      创建 人员
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>
            <input class="submitcss" type="submit" value="创建" />
            <input class="submitcss" type="button" onclick="BackList('SysPerson')" value="返回" />
        </legend>
        <div class="bigdiv">
                 
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Name) %>
                <%: Html.ValidationMessageFor(model => model.Name) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.MyName) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.MyName) %>
                <%: Html.ValidationMessageFor(model => model.MyName) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Password) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Password) %>
                <%: Html.ValidationMessageFor(model => model.Password) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.SurePassword) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.SurePassword) %>
                <%: Html.ValidationMessageFor(model => model.SurePassword) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.MobilePhoneNumber) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.MobilePhoneNumber) %>
                <%: Html.ValidationMessageFor(model => model.MobilePhoneNumber) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.PhoneNumber) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.PhoneNumber) %>
                <%: Html.ValidationMessageFor(model => model.PhoneNumber) %>
            </div>        
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Province) %>
            </div>
            <div class="editor-field">
                <%=Html.DropDownListFor(model => model.Province,App.Models.SysFieldModels.GetSysField("SysPerson","Province"),"请选择")%>  
            </div>        
            <div class="editor-label">
                <%: Html.LabelFor(model => model.City) %>
            </div>
            <div class="editor-field">
                <select id="City" name="City">
                </select>              
            </div>        
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Village) %>
            </div>
            <div class="editor-field">
                <select id="Village" name="Village">
                </select>              
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Address) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Address) %>
                <%: Html.ValidationMessageFor(model => model.Address) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EmailAddress) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.EmailAddress) %>
                <%: Html.ValidationMessageFor(model => model.EmailAddress) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Remark) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Remark) %>
                <%: Html.ValidationMessageFor(model => model.Remark) %>
            </div>        
            <div class="editor-label">
                <%: Html.LabelFor(model => model.State) %>
            </div>
            <div class="editor-field">
             <%: Html.RadioButtonListFor(model => model.State,App.Models.SysFieldModels.GetSysField("SysPerson","State"),false) %>
            </div>   <div class="editor-label">
            <a class="anUnderLine" onclick="showModalMany('SysRoleId','../../SysRole');">
                <%: Html.LabelFor(model => model.SysRoleId) %>
            </a>
        </div>
        <div class="editor-field">
            <div id="checkSysRoleId">
                <% 
                if (Model != null && !string.IsNullOrWhiteSpace(Model.SysRoleId))
                {
                   foreach (var item20 in Model.SysRoleId.Split('^'))
                   {
                        string[] it = item20.Split('&');
                        if (it != null && it.Length == 2 && !string.IsNullOrWhiteSpace(it[0]) && !string.IsNullOrWhiteSpace(it[1]))
                        {                        
                %>
                <table id="<%: item20 %>"
                    class="deleteStyle">
                    <tr>
                        <td>
                            <img  alt="删除" title="点击删除" onclick="deleteTable('<%: item20  %>','SysRoleId');"  src="../../../Images/deleteimge.png" />
                        </td>
                        <td>
                            <%: it[1] %>
                        </td>
                    </tr>
                </table>
                <%}}}%>
               <%: Html.HiddenFor(model => model.SysRoleId) %>
            </div>
        </div>           
            <div class="editor-label">
                <input id="file_upload" name="file_upload" type="file" />
            </div>
            <div id="up" class="editor-field">
                <div id="fileQueue">
                </div>
                <%: Html.HiddenFor(model => model.FileUploaderId) %>                
            </div> 
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    
    <link href="../../Res/jquery.uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../../Res/jquery.uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
    <script src="../../Res/jquery.uploadify-v2.1.4/swfobject.js" type="text/javascript"></script>     

    <script type="text/javascript">

        $(function () {
            
            getCity("#City");
            $("#Province").change(function () { getCity("#City"); });

            getVillage("#Village");
            $("#City").change(function () { getVillage("#Village"); });
 
                $('#file_upload').uploadify({
                    'uploader': '../../Res/jquery.uploadify-v2.1.4/uploadify.swf',
                    'script': '../../Res/jquery.uploadify-v2.1.4/FileUploader.ashx',
                    'cancelImg': '../../Res/jquery.uploadify-v2.1.4/cancel.png',
                    'folder': '/up',
                    'queueID': 'fileQueue',
                    'auto': true,
                    'multi': true,
                    'simUploadLimit': 5,                
                    'onComplete': function (event, queueId, fileObj, response, data) {
                         if (response == '0') {
                         alert('上传失败，请检查文件是否存在或者网络是否通畅');
                         return;
                    }
                    var content = ""; //需要添加的内容
                    var hidden = document.getElementById('FileUploaderId'); //获取隐藏的控件
                    hidden.value += "^" + response + "&" + fileObj.name;//为隐藏控件赋值
                    content += '<table  id="' + response + '" class="deleteStyle"><tr><td><img src="../../../Images/deleteimge.png" title="点击删除"  alt="删除"    onclick=" deleteTable(' + "'" + response + "'," + "'" + 'FileUploaderId' + "'" + ');" /></td><td>' + fileObj.name + '</td></tr></table>';
                     if (content!=null) {   
                         $("#up").append(content); 
                    }
                           
                    }                
                });


        });
        
        function getCity(City) {
            $(City).empty();
            $("<option></option>")
                    .val("")
                    .text("请选择")
                    .appendTo($(City));
            bindDropDownList(City, "#Province");
            $(City).change();
        }

        function getVillage(Village) {
            $(Village).empty();
            $("<option></option>")
                    .val("")
                    .text("请选择")
                    .appendTo($(Village));
            bindDropDownList(Village, "#City");
            $(Village).change();
        }
      

    </script>
</asp:Content>

