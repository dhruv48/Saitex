<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="CustomerRequestForFabric.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CustomerRequestForFabric"
    Title="Untitled Page" %>


<%@ Register src="../Controls/CustomerRequestFabric.ascx" tagname="CustomerRequestFabric" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:CustomerRequestFabric ID="CustomerRequestFabric1" runat="server" />
</asp:Content>
