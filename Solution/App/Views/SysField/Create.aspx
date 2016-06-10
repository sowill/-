﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Create.Master" Inherits="System.Web.Mvc.ViewPage<CHAIN.DAL.SysField>" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="CHAIN.App.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CurentPlace" runat="server">
      创建 数据字典
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>
            <input class="submitcss" type="submit" value="创建" />
            <input class="submitcss" type="button" onclick="BackList('SysField')" value="返回" />
        </legend>
        <div class="bigdiv">
                 
            <div class="editor-label">
                <%: Html.LabelFor(model => model.MyTexts) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.MyTexts) %>
                <%: Html.ValidationMessageFor(model => model.MyTexts) %>
            </div>
        <div class="editor-label">
            <a class="anUnderLine" onclick="showModalOnly('ParentId','../../SysField');">
                <%: Html.LabelFor(model => model.ParentId) %>
            </a>
        </div>
        <div class="editor-field">
            <div id="checkParentId">               
            </div>
            <%: Html.HiddenFor(model => model.ParentId)%>
        </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.MyTables) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.MyTables) %>
                <%: Html.ValidationMessageFor(model => model.MyTables) %>
            </div>     
            <div class="editor-label">
                <%: Html.LabelFor(model => model.MyColums) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.MyColums) %>
                <%: Html.ValidationMessageFor(model => model.MyColums) %>
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
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script type="text/javascript">

        $(function () {
            

        });
              

    </script>
</asp:Content>

