﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Create.Master" Inherits="System.Web.Mvc.ViewPage<CHAIN.DAL.FileUploader>" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="CHAIN.App.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CurentPlace" runat="server">
      创建 附件
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>
            <input class="submitcss" type="submit" value="创建" />
            <input class="submitcss" type="button" onclick="BackList('FileUploader')" value="返回" />
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
            </div>   <div class="editor-label">
            <a class="anUnderLine" onclick="showModalMany('SysPersonId','../../SysPerson');">
                <%: Html.LabelFor(model => model.SysPersonId) %>
            </a>
        </div>
        <div class="editor-field">
            <div id="checkSysPersonId">
                <% 
                if (Model != null && !string.IsNullOrWhiteSpace(Model.SysPersonId))
                {
                   foreach (var item11 in Model.SysPersonId.Split('^'))
                   {
                        string[] it = item11.Split('&');
                        if (it != null && it.Length == 2 && !string.IsNullOrWhiteSpace(it[0]) && !string.IsNullOrWhiteSpace(it[1]))
                        {                        
                %>
                <table id="<%: item11 %>"
                    class="deleteStyle">
                    <tr>
                        <td>
                            <img  alt="删除" title="点击删除" onclick="deleteTable('<%: item11  %>','SysPersonId');"  src="../../../Images/deleteimge.png" />
                        </td>
                        <td>
                            <%: it[1] %>
                        </td>
                    </tr>
                </table>
                <%}}}%>
               <%: Html.HiddenFor(model => model.SysPersonId) %>
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

