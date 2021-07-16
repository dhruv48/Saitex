<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="InvoiceAgainstProductionOrder.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_YarnIssueAgainstProductionOrder" Title="Untitled Page" %>

<%@ Register src="../Controls/YarnIssueAgainstProductionOrder.ascx" tagname="YarnIssueAgainstProductionOrder" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:YarnIssueAgainstProductionOrder ID="YarnIssueAgainstProductionOrder1" 
        runat="server" />
</asp:Content>

