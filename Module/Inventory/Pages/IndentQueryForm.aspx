<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="IndentQueryForm.aspx.cs" Inherits="Inventory_Indent" Title="Untitled Page" %>

<%@ Register src="../Controls/MaterialIndentQueryForm.ascx" tagname="MaterialIndentQueryForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:MaterialIndentQueryForm ID="MaterialIndentQueryForm1" runat="server" />
</asp:Content>
