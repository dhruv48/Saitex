<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="IssueAgainstWorkOrder.aspx.cs" Inherits="Module_WorkOrder_Pages_IssueAgainstWorkOrder" Title="Untitled Page" %>
<%@ Register src="../Controls/IssueAgainstWorkOrder.ascx" tagname="IssueAgainstWorkOrder" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:IssueAgainstWorkOrder ID="IssueAgainstWorkOrder1" runat="server" />
</asp:Content>

