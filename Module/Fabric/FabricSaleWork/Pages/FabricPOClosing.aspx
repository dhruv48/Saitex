<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricPOClosing.aspx.cs" Inherits="Module_Inventory_Pages_FabricPOClosing" Title="Untitled Page" %>


<%@ Register src="../Controls/FabricPOClosing.ascx" tagname="FabricPOClosing" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
  
    <uc1:FabricPOClosing ID="FabricPOClosing1" runat="server" />
  
</asp:Content>

