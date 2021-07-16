<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricReceivingMisc.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_FabricReceivingMisc" %>

<%@ Register src="../Controls/Fabric_Receipt_Misc.ascx" tagname="Fabric_Receipt_Misc" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Fabric_Receipt_Misc ID="Fabric_Receipt_Misc1" runat="server" />
</asp:Content>

