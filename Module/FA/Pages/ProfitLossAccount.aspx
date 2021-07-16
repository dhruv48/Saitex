<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ProfitLossAccount.aspx.cs" Inherits="Module_FA_Pages_ProfitLossAccount" Title="Untitled Page" %>

<%@ Register src="../Controls/Profit_Loss.ascx" tagname="Profit_Loss" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Profit_Loss ID="Profit_Loss1" runat="server" />
</asp:Content>

