<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Invoice.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_Invoice" Title="Untitled Page" %>
<%@ Register src="~/Module/OrderDevelopment/CustomerRequest/Controls/Invoice.ascx" tagname="Invoice" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:Invoice ID="Invoice" runat="server" />
</asp:Content>

