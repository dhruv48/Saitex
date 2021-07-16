<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CustomerRequestForFabric.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CustomerRequestForSpinningThread" Title="Untitled Page" %>


<%@ Register src="../Controls/CustomerRequestForFabric.ascx" tagname="CustomerRequestForFabric" tagprefix="uc2" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:CustomerRequestForFabric ID="CustomerRequestForFabric1" runat="server" />
</asp:Content>

