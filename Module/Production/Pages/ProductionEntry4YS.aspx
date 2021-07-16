<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ProductionEntry4YS.aspx.cs" Inherits="Module_Production_Pages_ProductionEntry4YS" %>

<%@ Register src="../Queries/ProductionEntry4YS.ascx" tagname="ProductionEntry4YS" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ProductionEntry4YS ID="ProductionEntry4YS1" runat="server" />
</asp:Content>

