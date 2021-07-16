<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CRQueryForFabric.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Query_CRQueryForFabric" %>

<%@ Register src="../Controls/CRQueryForFabric.ascx" tagname="CRQueryForFabric" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:CRQueryForFabric ID="CRQueryForFabric1" runat="server" />
</asp:Content>

