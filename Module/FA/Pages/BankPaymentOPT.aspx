<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="BankPaymentOPT.aspx.cs" Inherits="Module_FA_Pages_BankPaymentOPT" Title="Untitled Page" %>

<%@ Register src="../Controls/BankPaymentReport.ascx" tagname="BankPaymentReport" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:BankPaymentReport ID="BankPaymentReport1" runat="server" />
</asp:Content>
