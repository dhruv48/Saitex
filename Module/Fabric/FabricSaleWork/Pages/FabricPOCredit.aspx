<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricPOCredit.aspx.cs" Inherits="Module_Inventory_Pages_FabricPOCredit" Title="Untitled Page" %>


<%@ Register src="../Controls/FabricPOCredit.ascx" tagname="FabricPOCredit" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    
    <uc1:FabricPOCredit ID="FabricPOCredit1" runat="server" />
    
</asp:Content>

