<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricStockDetailQuery.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_FabricStockDetailQuery" %>

<%@ Register src="../Controls/FabricStockDetail.ascx" tagname="FabricStockDetail" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FabricStockDetail ID="FabricStockDetail1" runat="server" />
</asp:Content>

