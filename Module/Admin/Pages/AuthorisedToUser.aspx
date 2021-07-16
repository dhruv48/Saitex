<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="AuthorisedToUser.aspx.cs" ValidateRequest="false" Inherits="Admin_AuthorisedToUser" Title="Untitled Page" %>

<%@ Register Src="~/Module/Admin/Controls/AuthorisedToUser.ascx" TagName="AuthorisedToUser"
    TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:AuthorisedToUser ID="AuthorisedToUser1" runat="server" />
  
</asp:Content>
