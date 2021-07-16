<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MaterialIssue.aspx.cs" Inherits="Module_Inventory_Queries_MaterialIssue" Title="Untitled Page" %>

<%@ Register src="../Controls/MaterialIssue.ascx" tagname="MaterialIssue" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:MaterialIssue ID="MaterialIssue1" runat="server" />
</asp:Content>