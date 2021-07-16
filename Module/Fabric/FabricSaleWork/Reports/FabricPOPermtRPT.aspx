<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricPOPermtRPT.aspx.cs" Inherits="Module_Inventory_Reports_FabricPOPermtRPT" Title="Untitled Page" %>


<%@ Register src="../Controls/FabricPOPermrpt.ascx" tagname="FabricPOPermrpt" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
   
    <uc1:FabricPOPermrpt ID="FabricPOPermrpt1" runat="server" />
   
</asp:Content>

