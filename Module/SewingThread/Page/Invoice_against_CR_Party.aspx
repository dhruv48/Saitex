<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Invoice_against_CR_Party.aspx.cs" Inherits="Module_SewingThread_Page_Invoice_against_CR_Party" %>

<%@ Register src="../Controls/Invoice_Against_CR_Party.ascx" tagname="Invoice_Against_CR_Party" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Invoice_Against_CR_Party ID="Invoice_Against_CR_Party1" runat="server" />
</asp:Content>

