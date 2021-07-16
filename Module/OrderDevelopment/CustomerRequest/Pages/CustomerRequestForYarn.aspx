<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CustomerRequestForYarn.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CustomerRequestForYarn" Title="Untitled Page" %>

<%@ Register src="../Controls/CustomerRequestForYarn.ascx" tagname="CustomerRequestForYarn" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:CustomerRequestForYarn ID="CustomerRequestForYarn1" runat="server" />
</asp:Content>

