
<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SalesOrderMachine.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_SalesOrderMachine" Title="Untitled Page" %>
<%@ Register src="../Controls/SalesOrderMachine.ascx" tagname="SalesOrderMachine" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:SalesOrderMachine ID="SalesOrderMachine" runat="server" />
</asp:Content>

