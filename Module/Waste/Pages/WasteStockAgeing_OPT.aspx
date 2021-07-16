<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="WasteStockAgeing_OPT.aspx.cs" Inherits="Module_Waste_Reports_WasteStockAgeing_OPT" %>

<%@ Register Src="../Controls/WasteStockAgeing.ascx" TagName="WasteStockAgeing"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:WasteStockAgeing ID="WasteStockAgeing1" runat="server" />
</asp:Content>
