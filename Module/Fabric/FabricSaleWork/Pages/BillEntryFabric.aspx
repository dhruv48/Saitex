<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="BillEntryFabric.aspx.cs" Inherits="Module_Inventory_Pages_BillEntryFabric" Title="Untitled Page" %>


<%@ Register src="../Controls/BillEntryFabric.ascx" tagname="BillEntryFabric" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 
    <uc1:BillEntryFabric ID="BillEntryFabric1" runat="server" />
 
</asp:Content>

