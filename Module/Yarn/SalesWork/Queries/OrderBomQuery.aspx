<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OrderBomQuery.aspx.cs" Inherits="Module_Yarn_SalesWork_Queries_OrderBomQuery" %>

<%@ Register src="../Controls/Order_bom_query.ascx" tagname="Order_bom_query" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Order_bom_query ID="Order_bom_query1" runat="server" />
</asp:Content>

