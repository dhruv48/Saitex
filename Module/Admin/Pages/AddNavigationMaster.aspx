<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="AddNavigationMaster.aspx.cs" Inherits="Module_Admin_Pages_AddNavigationMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/NavigationMaster.ascx" tagname="NavigationMaster" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

    <uc1:NavigationMaster ID="NavigationMaster1" runat="server" />

</asp:Content>

