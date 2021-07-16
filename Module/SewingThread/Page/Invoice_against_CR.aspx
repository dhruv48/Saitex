<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Invoice_against_CR.aspx.cs" Inherits="Module_SewingThread_Page_Invoice_against_CR" %>

<%@ Register src="../Controls/Invoice_against_CR.ascx" tagname="Invoice_against_CR" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Invoice_against_CR ID="Invoice_against_CR1" runat="server" />
</asp:Content>

