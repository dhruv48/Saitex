<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ItemStockQuery.aspx.cs" Inherits="Module_Inventory_Queries_ItemStockQuery" Title="Untitled Page" %>

<%@ Register src="../Controls/ItemStockQuery.ascx" tagname="ItemStockQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <uc1:ItemStockQuery ID="ItemStockQuery1" runat="server" />
</asp:Content>

