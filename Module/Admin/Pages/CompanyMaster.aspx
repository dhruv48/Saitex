<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CompanyMaster.aspx.cs" Inherits="Module_Admin_Pages_CompanyMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/CompanyMaster.ascx" tagname="CompanyMaster" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:CompanyMaster ID="CompanyMaster1" runat="server" />
</asp:Content>

