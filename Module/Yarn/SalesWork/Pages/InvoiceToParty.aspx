<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="InvoiceToParty.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_InvoiceToParty" %>
<%@ Register src="../Controls/InvoiceToParty.ascx" tagname="InvoiceToParty" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:InvoiceToParty ID="InvoiceToParty1" runat="server" />
</asp:Content>

