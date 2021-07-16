<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MaterialReceiptMiss.aspx.cs" Inherits="Inventory_MaterialReceiptMiss" Title="Untitled Page" %>

<%@ Register src="../Controls/ReceiptMiss.ascx" tagname="ReceiptMiss" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
  
    <uc1:ReceiptMiss ID="ReceiptMiss1" runat="server" />
  
</asp:Content>

