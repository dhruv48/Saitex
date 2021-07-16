<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricReceivingAgainstCredit.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_FabricReceivingAgainstCredit" %>

<%@ Register src="../Controls/Fabric_Receipt_PO.ascx" tagname="Fabric_Receipt_PO" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Fabric_Receipt_PO ID="Fabric_Receipt_PO1" runat="server" />
</asp:Content>

