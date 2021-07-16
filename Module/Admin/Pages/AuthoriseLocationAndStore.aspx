<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="AuthoriseLocationAndStore.aspx.cs" Inherits="Module_Admin_Pages_AuthoriseLocationAndStore" %>

<%@ Register Src="~/Module/Admin/Controls/AuthoriseLocationAndStore.ascx" TagName="AuthorisedToUser"
    TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:AuthorisedToUser ID="AuthorisedToUser1" runat="server" />
  
</asp:Content>