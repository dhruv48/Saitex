<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MaterialReceiptAgainstPOCash.aspx.cs" Inherits="Inventory_MaterialReceiptAgainstPOCash" Title="Untitled Page" %>

<%@ Register src="../Controls/ReceiptPOCash.ascx" tagname="ReceiptPOCash" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:ReceiptPOCash ID="ReceiptPOCash1" runat="server" />
</asp:Content>

