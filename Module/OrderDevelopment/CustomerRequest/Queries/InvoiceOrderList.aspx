<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="InvoiceOrderList.aspx.cs"
 Inherits="Module_OrderDevelopment_Queries_InvoiceOrderList" %>
 <%@ Register src="../Controls/InvoiceOrderList.ascx" tagname="InvoiceOrderList" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <uc2:InvoiceOrderList ID="InvoiceOrderList1" runat="server" />
</asp:Content>

