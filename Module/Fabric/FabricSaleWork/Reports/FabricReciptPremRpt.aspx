<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricReciptPremRpt.aspx.cs" Inherits="Module_Inventory_Reports_FabricREciptPremRpt" Title="Untitled Page" %>


<%@ Register src="../Controls/FabricReciptPremRpt.ascx" tagname="FabricReciptPremRpt" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
  
    <uc1:FabricReciptPremRpt ID="FabricReciptPremRpt1" runat="server" />
  
</asp:Content>

