<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MaterialLedgerQueryForm.aspx.cs" Inherits="Module_Inventory_Pages_MaterialLedgerQueryForm" Title="Untitled Page" %>

<%@ Register src="../Controls/MaterialLedgerQueryForm.ascx" tagname="MaterialLedgerQueryForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:MaterialLedgerQueryForm ID="MaterialLedgerQueryForm1" runat="server" />
</asp:Content>

