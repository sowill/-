<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Edit.Master" Inherits="System.Web.Mvc.ViewPage<CHAIN.DAL.SysRole>" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="CHAIN.App.Models" %>
<asp:Content ID="Content4" ContentPlaceHolderID="CurentPlace" runat="server">
    修改 角色
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>
            <input class="submitcss" type="submit" value="修改" />
            <input class="submitcss" type="button" onclick="BackList('SysRole')" value="返回列表" />
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
            <br style="clear: both;" />
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Description) %>
            </div>
            <div class="textarea-box">
                <%: Html.TextAreaFor(model => model.Description) %>
                <%: Html.ValidationMessageFor(model => model.Description) %>
            </div>
            <br style="clear: both;" />
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Remark) %>
            </div>
            <div class="textarea-box">
                <%: Html.TextAreaFor(model => model.Remark) %>
                <%: Html.ValidationMessageFor(model => model.Remark) %>
            </div><%: Html.HiddenFor(model => model.CreateTime ) %><%: Html.HiddenFor(model => model.CreatePerson ) %>  
        <div class="editor-label">
            <a class="anUnderLine" onclick="showModalMany('SysPersonId','../../SysPerson');">
                <%: Html.LabelFor(model => model.SysPersonId) %>
            </a>
        </div>
        <div class="editor-field">
            <div id="checkSysPersonId">
                <% string ids9 = string.Empty;
                if(Model!=null)
                {
                   foreach (var item9 in Model.SysPerson)
                   {
                       string item91 = string.Empty;
                       item91 += item9.Id + "&" + item9.Name;
                       if (ids9.Length > 0)
                       {
                           ids9 += "^" + item91;
                       }
                       else
                       {
                           ids9 += item91;
                       }
                %>
                <table id="<%: item91 %>"
                    class="deleteStyle">
                    <tr>
                        <td>
                            <img  alt="删除" title="点击删除" onclick="deleteTable('<%: item91 %>','SysPersonId');"  src="../../../Images/deleteimge.png" />
                        </td>
                        <td>
                            <%: item9.Name %>
                        </td>
                    </tr>
                </table>
                <%}}%>
                <input type="hidden" value="<%= ids9 %>" name="SysPersonIdOld" id="SysPersonIdOld" />
                <input type="hidden" value="<%= ids9 %>" name="SysPersonId" id="SysPersonId" />
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

