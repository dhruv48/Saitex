<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Trading.aspx.cs" Inherits="Module_FA_Pages_Trading" Title="Untitled Page" %>

<%@ Register src="../Controls/TradingAccount.ascx" tagname="TradingAccount" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:TradingAccount ID="TradingAccount1" runat="server" />
</asp:Content>

