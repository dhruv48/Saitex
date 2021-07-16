
<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="From_Stock_Sales1.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_From_Stock_Sales1" %>
<%@ Register Src="~/Module/OrderDevelopment/CustomerRequest/Controls/From_Stock_Sales.ascx" TagName="From_Stock_Sales1" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:From_Stock_Sales1 id="From_Stock_Sales1" runat = "server"></uc1:From_Stock_Sales1>
</asp:Content>

