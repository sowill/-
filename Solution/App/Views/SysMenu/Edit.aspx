<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Edit.Master" Inherits="System.Web.Mvc.ViewPage<CHAIN.DAL.SysMenu>" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="CHAIN.App.Models" %>
<asp:Content ID="Content4" ContentPlaceHolderID="CurentPlace" runat="server">
    修改 模块
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>
            <input class="submitcss" type="submit" value="修改" />
            <input class="submitcss" type="button" onclick="BackList('SysMenu')" value="返回列表" />
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
                <a class="anUnderLine" onclick="showModalOnly('ParentId','../../SysMenu');">
                    <%: Html.LabelFor(model => model.ParentId) %>
                </a>
            </div>
            <div class="editor-field">
                <div id="checkParentId">
                    <% if(Model!=null)
                        {
                        if (!string.IsNullOrWhiteSpace(Model.ParentId))
                        {%>
                    <table  id="<%: Model.ParentId %>"
                        class="deleteStyle">
                        <tr>
                            <td>
                                <img  alt="删除"  title="点击删除" src="../../../Images/deleteimge.png" onclick="deleteTable('<%: Model.ParentId %>','ParentId');"/>
                            </td>
                            <td>
                                <%: Model.SysMenu2.Name%>
                            </td>
                        </tr>
                    </table>
                    <%}}%>
                </div>
                <%: Html.HiddenFor(model => model.ParentId)%>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Url) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Url) %>
                <%: Html.ValidationMessageFor(model => model.Url) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Iconic) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Iconic) %>
                <%: Html.ValidationMessageFor(model => model.Iconic) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Sort) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Sort, new {  onkeyup = "isInt(this)", @class="text-box single-line" })%>
                <%: Html.ValidationMessageFor(model => model.Sort) %>
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
                <%=Html.DropDownListFor(model => model.State,App.Models.SysFieldModels.GetSysField("SysMenu","State"),"请选择")%>  
            </div><%: Html.HiddenFor(model => model.CreatePerson ) %><%: Html.HiddenFor(model => model.CreateTime ) %>  
        <div class="editor-label">
            <a class="anUnderLine" onclick="showModalMany('SysRoleId','../../SysRole');">
                <%: Html.LabelFor(model => model.SysRoleId) %>
            </a>
        </div>
        <div class="editor-field">
            <div id="checkSysRoleId">
                <% string ids13 = string.Empty;
                if(Model!=null)
                {
                   foreach (var item13 in Model.SysRole)
                   {
                       string item131 = string.Empty;
                       item131 += item13.Id + "&" + item13.Name;
                       if (ids13.Length > 0)
                       {
                           ids13 += "^" + item131;
                       }
                       else
                       {
                           ids13 += item131;
                       }
                %>
                <table id="<%: item131 %>"
                    class="deleteStyle">
                    <tr>
                        <td>
                            <img  alt="删除" title="点击删除" onclick="deleteTable('<%: item131 %>','SysRoleId');"  src="../../../Images/deleteimge.png" />
                        </td>
                        <td>
                            <%: item13.Name %>
                        </td>
                    </tr>
                </table>
                <%}}%>
                <input type="hidden" value="<%= ids13 %>" name="SysRoleIdOld" id="SysRoleIdOld" />
                <input type="hidden" value="<%= ids13 %>" name="SysRoleId" id="SysRoleId" />
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

