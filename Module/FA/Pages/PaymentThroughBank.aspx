<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="PaymentThroughBank.aspx.cs" Inherits="Module_FA_Pages_PaymentThroughBank" Title="Untitled Page" %>

<%@ Register src="../Controls/BankPayments.ascx" tagname="BankPayments" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <uc1:BankPayments ID="BankPayments1" runat="server" />
</asp:Content>

