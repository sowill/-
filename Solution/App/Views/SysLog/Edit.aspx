<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Edit.Master" Inherits="System.Web.Mvc.ViewPage<CHAIN.DAL.SysLog>" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="CHAIN.App.Models" %>
<asp:Content ID="Content4" ContentPlaceHolderID="CurentPlace" runat="server">
    修改 日志
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>
            <input class="submitcss" type="submit" value="修改" />
            <input class="submitcss" type="button" onclick="BackList('SysLog')" value="返回列表" />
        </legend>
        <div class="bigdiv">
            <%: Html.HiddenFor(model => model.Id ) %>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.PersonId) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.PersonId) %>
                <%: Html.ValidationMessageFor(model => model.PersonId) %>
            </div>
            <br style="clear: both;" />
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Message) %>
            </div>
            <div class="textarea-box">
                <%: Html.TextAreaFor(model => model.Message) %>
                <%: Html.ValidationMessageFor(model => model.Message) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Result) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Result) %>
                <%: Html.ValidationMessageFor(model => model.Result) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.MenuId) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.MenuId) %>
                <%: Html.ValidationMessageFor(model => model.MenuId) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Ip) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Ip) %>
                <%: Html.ValidationMessageFor(model => model.Ip) %>
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
                <%=Html.DropDownListFor(model => model.State,App.Models.SysFieldModels.GetSysField("SysLog","State"),"请选择")%>  
            </div><%: Html.HiddenFor(model => model.CreateTime ) %><%: Html.HiddenFor(model => model.CreatePerson ) %>
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script type="text/javascript">

        $(function () {
            

        });
              

    </script>
</asp:Content>

