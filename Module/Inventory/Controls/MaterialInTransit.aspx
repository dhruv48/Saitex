<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MaterialInTransit.aspx.cs" Inherits="Module_Inventory_Pages_MaterialInTransit" %>
<%@ Register src="~/Module/Inventory/Controls/MaterialInTransit.ascx" tagname="MaterialInTransit" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
      <uc1:MaterialInTransit ID="MaterialInTransit" runat="server" />
</asp:Content>

