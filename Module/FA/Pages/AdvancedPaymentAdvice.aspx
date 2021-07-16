<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="AdvancedPaymentAdvice.aspx.cs" Inherits="Module_FA_Pages_AdvancedPaymentAdvice" Title="Untitled Page" %>

<%@ Register src="../Controls/AdvancedAdvice.ascx" tagname="AdvancedAdvice" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:AdvancedAdvice ID="AdvancedAdvice1" runat="server" />
</asp:Content>

