<%@ Page Language="C#" MasterPageFile="~/CommonMaster/UserMaster.master" AutoEventWireup="true"
    CodeFile="GetUserAuthorisation.aspx.cs" Inherits="GetUserAuthorisation" Title="User Authorization" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Module/Admin/Controls/GetUserAuthorisation.ascx" TagName="GetUserAuthorisation"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
   <%-- <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>--%>
    <br />
    <br />
    <uc1:GetUserAuthorisation ID="GetUserAuthorisation1" runat="server" />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
