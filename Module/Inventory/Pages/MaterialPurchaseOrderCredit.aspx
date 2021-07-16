<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="MaterialPurchaseOrderCredit.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Inventory_MaterialPurchaseOrderCredit"
    Title="PO Material Credit" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/POCredit.ascx" TagName="POCredit" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:POCredit ID="POCredit1" runat="server" />
</asp:Content>
