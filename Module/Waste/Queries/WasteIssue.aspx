<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="WasteIssue.aspx.cs" Inherits="Module_Waste_Queries_WasteIssue" Title="Untitled Page" %>

<%@ Register src="../Controls/WasteIssue.ascx" tagname="MaterialIssue" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:MaterialIssue ID="MaterialIssue1" runat="server" />
</asp:Content>