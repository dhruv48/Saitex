<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="IndentQueryForm.aspx.cs" Inherits="Module_Inventory_Queries_IndentQueryForm" %>

<%@ Register src="../Controls/MaterialIndentQueryForm.ascx" tagname="MaterialIndentQueryForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:MaterialIndentQueryForm ID="MaterialIndentQueryForm1" runat="server" />
</asp:Content>
