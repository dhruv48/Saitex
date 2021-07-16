<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="YrnIssue.aspx.cs" Inherits="Module_Yarn_SalesWork_Queries_YrnIssue" Title="Untitled Page" %>

<%@ Register src="../Controls/YrnIssue.ascx" tagname="YrnIssue" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:YrnIssue ID="YrnIssue1" runat="server" />
</asp:Content>

