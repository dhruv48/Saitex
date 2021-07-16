<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="BranchQuery.aspx.cs" Inherits="Module_Admin_Queries_BranchQuery" %>

<%@ Register src="../../../CommonControls/QueryGridView.ascx" tagname="QueryGridView" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:QueryGridView ID="QueryGridView1" runat="server" />
</asp:Content>

