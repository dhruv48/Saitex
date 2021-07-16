<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="UserNavigationRight.aspx.cs" Inherits="Admin_UserNavigationRight" Title="Untitled Page" %>

<%@ Register Src="~/Module/Admin/Controls/UserAccessRightNew.ascx" TagName="UserAccessRight" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:UserAccessRight ID="UserAccessRight1" runat="server" />
</asp:Content>

