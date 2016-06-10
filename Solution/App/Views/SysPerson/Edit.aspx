<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Edit.Master" Inherits="System.Web.Mvc.ViewPage<CHAIN.DAL.SysPerson>" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="CHAIN.App.Models" %>
<asp:Content ID="Content4" ContentPlaceHolderID="CurentPlace" runat="server">
    修改 人员
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>
            <input class="submitcss" type="submit" value="修改" />
            <input class="submitcss" type="button" onclick="BackList('SysPerson')" value="返回列表" />
        </legend>
        <div class="bigdiv">
            <%: Html.HiddenFor(model => model.Id ) %>     
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
                <%=Html.DropDownListFor(model => model.City,App.Models.SysFieldModels.GetSysField("SysPerson","City",Model.Province),"请选择")%>  
            </div>        
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Village) %>
            </div>
            <div class="editor-field">
                <%=Html.DropDownListFor(model => model.Village,App.Models.SysFieldModels.GetSysField("SysPerson","Village",Model.City),"请选择")%>  
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
            </div><%: Html.HiddenFor(model => model.CreateTime ) %><%: Html.HiddenFor(model => model.CreatePerson ) %><%: Html.HiddenFor(model => model.Version ) %>  
        <div class="editor-label">
            <a class="anUnderLine" onclick="showModalMany('SysRoleId','../../SysRole');">
                <%: Html.LabelFor(model => model.SysRoleId) %>
            </a>
        </div>
        <div class="editor-field">
            <div id="checkSysRoleId">
                <% string ids20 = string.Empty;
                if(Model!=null)
                {
                   foreach (var item20 in Model.SysRole)
                   {
                       string item201 = string.Empty;
                       item201 += item20.Id + "&" + item20.Name;
                       if (ids20.Length > 0)
                       {
                           ids20 += "^" + item201;
                       }
                       else
                       {
                           ids20 += item201;
                       }
                %>
                <table id="<%: item201 %>"
                    class="deleteStyle">
                    <tr>
                        <td>
                            <img  alt="删除" title="点击删除" onclick="deleteTable('<%: item201 %>','SysRoleId');"  src="../../../Images/deleteimge.png" />
                        </td>
                        <td>
                            <%: item20.Name %>
                        </td>
                    </tr>
                </table>
                <%}}%>
                <input type="hidden" value="<%= ids20 %>" name="SysRoleIdOld" id="SysRoleIdOld" />
                <input type="hidden" value="<%= ids20 %>" name="SysRoleId" id="SysRoleId" />
            </div>
        </div>   
                <div class="editor-label">
                    <input id="file_upload" name="file_upload" type="file" />
                </div>
                <div id="up" class="editor-field"> <div id="fileQueue">
                    </div>
                <div id="checkFileUploaderId">
                    <% string ids21 = string.Empty;
                    if(Model!=null)
                        {
                       foreach (var item21 in Model.FileUploader)
                       {
                           string it = string.Empty;
                           it += item21.Id + "&" + item21.Name;
                           if (ids21.Length > 0)
                           {
                               ids21 += "^" + it;
                           }
                           else
                           {
                               ids21 += it;
                           }
 
                    %>
                    <table id="<%: it %>"
                        class="deleteStyle">
                        <tr>
                            <td>
                                <img  alt="删除" title="点击删除" onclick="deleteTable('<%: it %>','FileUploaderId');"  src="../../../Images/deleteimge.png" />
                            </td>
                            <td>
                                <%: item21.Name %>
                            </td>
                        </tr>
                    </table>
                    <%}}%>
                    <input type="hidden" value="<%= ids21 %>" name="FileUploaderId" id="FileUploaderId" />
                    <input type="hidden" value="<%= ids21 %>" name="FileUploaderIdOld" id="FileUploaderIdOld" />
                </div>
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
            
            
            $("#Province").change(function () { getCity("#City"); });

            
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

