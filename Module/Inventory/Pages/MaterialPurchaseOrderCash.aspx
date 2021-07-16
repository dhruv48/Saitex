<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="MaterialPurchaseOrderCash.aspx.cs" Inherits="Inventory_MaterialPurchaseOrderCash"
    Title="PO Meterail Cash" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/POCash.ascx" TagName="POCash" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:POCash ID="POCash1" runat="server" />
</asp:Content>
