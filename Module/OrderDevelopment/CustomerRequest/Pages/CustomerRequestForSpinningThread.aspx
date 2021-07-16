<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CustomerRequestForSpinningThread.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CustomerRequestForSpinningThread" Title="Untitled Page" %>

<%@ Register src="../Controls/CustomerRequestForSpinningThread.ascx" tagname="CustomerRequestForSpinningThread" tagprefix="uc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:CustomerRequestForSpinningThread ID="CustomerRequestForSpinningThread1" 
        runat="server" />
</asp:Content>

