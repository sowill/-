<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Details.Master"Inherits="System.Web.Mvc.ViewPage<CHAIN.DAL.SysMenu>" %>
<%@ Import Namespace="Common" %>
 
<asp:Content ID="Content4" ContentPlaceHolderID="CurentPlace" runat="server">
      详细 模块
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <fieldset>
        <legend>
            <input class="submitcss" type="button"  onclick="window.location.href = 'javascript:history.go(-1)';"  value="返回" />   
        </legend>
        <div class="bigdiv">
                  
                <div class="display-label">
                      <%: Html.LabelFor(model => model.Name) %>
                </div>
                <div class="display-field">
                    <%: Html.DisplayFor(model => model.Name) %>
                </div>        
                <div class="display-label">
                      <%: Html.LabelFor(model => model.ParentId) %>
                </div>
                <div class="display-field">
                    <% if (Model.SysMenu2 != null && !string.IsNullOrEmpty(Model.SysMenu2.Name))
                       { %>
                    <%: Model.SysMenu2.Name%>
                    <%} %>
                </div>      
                <div class="display-label">
                      <%: Html.LabelFor(model => model.Url) %>
                </div>
                <div class="display-field">
                    <%: Html.DisplayFor(model => model.Url) %>
                </div>      
                <div class="display-label">
                      <%: Html.LabelFor(model => model.Iconic) %>
                </div>
                <div class="display-field">
                    <%: Html.DisplayFor(model => model.Iconic) %>
                </div>      
                <div class="display-label">
                      <%: Html.LabelFor(model => model.Sort) %>
                </div>
                <div class="display-field">
                    <%: Html.DisplayFor(model => model.Sort) %>
                </div>
                <br style="clear: both;" />
                <div class="display-label">
                    <%: Html.LabelFor(model => model.Remark) %>
                </div>
                <div class="textarea-box">
                    <%: Html.TextAreaFor(model => model.Remark, new {  @readonly=true}) %>                  
                </div>      
                <div class="display-label">
                      <%: Html.LabelFor(model => model.State) %>
                </div>
                <div class="display-field">
                    <%: Html.DisplayFor(model => model.State) %>
                </div>      
                <div class="display-label">
                      <%: Html.LabelFor(model => model.CreatePerson) %>
                </div>
                <div class="display-field">
                    <%: Html.DisplayFor(model => model.CreatePerson) %>
                </div>      
                <div class="display-label">
                      <%: Html.LabelFor(model => model.CreateTime) %>
                </div>
                <div class="display-field">
                    <%: Html.DisplayFor(model => model.CreateTime) %>
                </div>      
                <div class="display-label">
                      <%: Html.LabelFor(model => model.UpdateTime) %>
                </div>
                <div class="display-field">
                    <%: Html.DisplayFor(model => model.UpdateTime) %>
                </div>      
                <div class="display-label">
                      <%: Html.LabelFor(model => model.UpdatePerson) %>
                </div>
                <div class="display-field">
                    <%: Html.DisplayFor(model => model.UpdatePerson) %>
                </div>    
                <div class="display-label">
                      <%: Html.LabelFor(model => model.SysRoleId) %>
                </div>
                <div class="display-field">            
                    <% string ids13 = string.Empty;
                       foreach (var item13 in Model.SysRole)
                       {
                           ids13 += item13.Name;
                           ids13 += " , ";
                    %>               
                    <%}%>
                    <div class="display-field">
                        <%= ids13 %>   
                    </div>
                </div>
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

