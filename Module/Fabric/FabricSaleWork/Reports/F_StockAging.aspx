<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="F_StockAging.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Reports_F_StockAging" %>

<%@ Register src="../Controls/FabricStockAging.ascx" tagname="FabricStockAging" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FabricStockAging ID="FabricStockAging1" runat="server" />
</asp:Content>

