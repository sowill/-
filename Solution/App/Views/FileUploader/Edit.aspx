<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Edit.Master" Inherits="System.Web.Mvc.ViewPage<CHAIN.DAL.FileUploader>" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="CHAIN.App.Models" %>
<asp:Content ID="Content4" ContentPlaceHolderID="CurentPlace" runat="server">
    修改 附件
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>
            <input class="submitcss" type="submit" value="修改" />
            <input class="submitcss" type="button" onclick="BackList('FileUploader')" value="返回列表" />
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
                <%: Html.LabelFor(model => model.Path) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Path) %>
                <%: Html.ValidationMessageFor(model => model.Path) %>
            </div>
            <br style="clear: both;" />
            <div class="editor-label">
                <%: Html.LabelFor(model => model.FullPath) %>
            </div>
            <div class="textarea-box">
                <%: Html.TextAreaFor(model => model.FullPath) %>
                <%: Html.ValidationMessageFor(model => model.FullPath) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Suffix) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Suffix) %>
                <%: Html.ValidationMessageFor(model => model.Suffix) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Size) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Size, new {  onkeyup = "isInt(this)", @class="text-box single-line" })%>
                <%: Html.ValidationMessageFor(model => model.Size) %>
            </div>
            <br style="clear: both;" />
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Remark) %>
            </div>
            <div class="textarea-box">
                <%: Html.TextAreaFor(model => model.Remark) %>
                <%: Html.ValidationMessageFor(model => model.Remark) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.State) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.State) %>
                <%: Html.ValidationMessageFor(model => model.State) %>
            </div><%: Html.HiddenFor(model => model.CreateTime ) %><%: Html.HiddenFor(model => model.CreatePerson ) %>  
        <div class="editor-label">
            <a class="anUnderLine" onclick="showModalMany('SysPersonId','../../SysPerson');">
                <%: Html.LabelFor(model => model.SysPersonId) %>
            </a>
        </div>
        <div class="editor-field">
            <div id="checkSysPersonId">
                <% string ids11 = string.Empty;
                if(Model!=null)
                {
                   foreach (var item11 in Model.SysPerson)
                   {
                       string item111 = string.Empty;
                       item111 += item11.Id + "&" + item11.Name;
                       if (ids11.Length > 0)
                       {
                           ids11 += "^" + item111;
                       }
                       else
                       {
                           ids11 += item111;
                       }
                %>
                <table id="<%: item111 %>"
                    class="deleteStyle">
                    <tr>
                        <td>
                            <img  alt="删除" title="点击删除" onclick="deleteTable('<%: item111 %>','SysPersonId');"  src="../../../Images/deleteimge.png" />
                        </td>
                        <td>
                            <%: item11.Name %>
                        </td>
                    </tr>
                </table>
                <%}}%>
                <input type="hidden" value="<%= ids11 %>" name="SysPersonIdOld" id="SysPersonIdOld" />
                <input type="hidden" value="<%= ids11 %>" name="SysPersonId" id="SysPersonId" />
            </div>
        </div>
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script type="text/javascript">

        $(function () {
            

        });
              

    </script>
</asp:Content>

