<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricLedgerQuery.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_FabricLedgerQuery" %>
<%@ Register Src="~/Module/Fabric/FabricSaleWork/Controls/FabricLedgerQuery1.ascx" TagName="FabricLedgerQuery" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:FabricLedgerQuery ID="FabricLedgerQuery2" runat="Server" />
</asp:Content>

