<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Module_Inventory_Pages_Test" Title="Untitled Page" %>

<%@ Register src="../Controls/Item_LOV.ascx" tagname="Item_LOV" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

     
    <uc1:Item_LOV ID="Item_LOV1" runat="server" />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>

