<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="BillEntryMaterial.aspx.cs" Inherits="Module_Inventory_Pages_BillEntryMaterial" Title="Untitled Page" %>

<%@ Register src="../Controls/BillEntryMaterial.ascx" tagname="BillEntryMaterial" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:BillEntryMaterial ID="BillEntryMaterial1" runat="server" />
</asp:Content>

