<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricPOApproval.aspx.cs" Inherits="Module_Inventory_Pages_FabricPOApproval" Title="Untitled Page" %>


<%@ Register src="../Controls/FabricPOApproval.ascx" tagname="FabricPOApproval" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
  
    <uc1:FabricPOApproval ID="FabricPOApproval1" runat="server" />
  
</asp:Content>

