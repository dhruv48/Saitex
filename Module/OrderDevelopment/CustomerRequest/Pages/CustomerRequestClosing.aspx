<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CustomerRequestClosing.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CustomerRequestClosing" %>

<%@ Register src="../Controls/CustomerRequestClose.ascx" tagname="CustomerRequestClose" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:CustomerRequestClose ID="CustomerRequestClose1" runat="server" />
</asp:Content>

