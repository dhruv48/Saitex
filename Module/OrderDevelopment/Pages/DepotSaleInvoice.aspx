<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="DepotSaleInvoice.aspx.cs" Inherits="Module_OrderDevelopment_Pages_DepotSaleInvoice" Title="Untitled Page" %>

<%@ Register src="../Controls/DepotSaleInvoice.ascx" tagname="DepotSaleInvoice" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:DepotSaleInvoice ID="DepotSaleInvoice1" runat="server" />
</asp:Content>

