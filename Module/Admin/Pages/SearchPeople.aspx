<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SearchPeople.aspx.cs" Inherits="Module_Admin_Pages_SearchPeople" Title="Untitled Page" %>

<%@ Register src="../../../CommonControls/Search_People.ascx" tagname="Search_People" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Search_People ID="Search_People1" runat="server" />
</asp:Content>

