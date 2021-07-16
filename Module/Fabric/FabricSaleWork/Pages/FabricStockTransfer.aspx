<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricStockTransfer.aspx.cs" Inherits="Module_Inventory_Pages_FabricStockTransfer" Title="Untitled Page" %>




<%@ Register src="../Controls/FabricStockTransferNew.ascx" tagname="FabricStockTransferNew" tagprefix="uc1" %>




<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 
    
 
    <uc1:FabricStockTransferNew ID="FabricStockTransferNew1" runat="server" />
 
    
 
</asp:Content>

