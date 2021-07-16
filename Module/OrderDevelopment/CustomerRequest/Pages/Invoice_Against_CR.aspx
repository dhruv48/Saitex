<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="Invoice_Against_CR.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_Invoice_Against_CR" %>
<%@ Register Src="~/Module/OrderDevelopment/CustomerRequest/Controls/Invoice_Against_CR.ascx" TagName="invoice" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">

   <uc1:invoice id="Invoice" runat = "server"></uc1:invoice>                             
</asp:Content>
