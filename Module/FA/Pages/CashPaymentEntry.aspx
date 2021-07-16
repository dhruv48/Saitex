<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CashPaymentEntry.aspx.cs" Inherits="Module_FA_Pages_CashPaymentEntry" Title="Untitled Page" %>

<%@ Register src="../Controls/CashPayment.ascx" tagname="CashPayment" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    
    <uc1:CashPayment ID="CashPayment1" runat="server" />
    
</asp:Content>

