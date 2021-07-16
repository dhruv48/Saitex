<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="MaterialStockAgeing_OPT.aspx.cs" Inherits="Module_Inventory_Reports_MaterialStockAgeing_OPT" %>

<%@ Register Src="../Controls/MatItemStockAgeing.ascx" TagName="MatItemStockAgeing"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:MatItemStockAgeing ID="MatItemStockAgeing1" runat="server" />
</asp:Content>
