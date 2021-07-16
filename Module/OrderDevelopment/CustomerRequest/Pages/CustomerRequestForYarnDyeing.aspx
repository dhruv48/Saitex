<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CustomerRequestForYarnDyeing.aspx.cs" 
Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CustomerRequestForYarnDyeing" %>
<%@ Register Src="~/Module/OrderDevelopment/CustomerRequest/Controls/CustermerRequestForYarnDyeing1.ascx" TagName = "CustomerRequestForYarnDyeing" TagPrefix = "uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:CustomerRequestForYarnDyeing ID = "CustomerRequestForYarnDyeing1" runat = "server" />
</asp:Content>

