<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="TrialBalanceOPT.aspx.cs" Inherits="Module_FA_Pages_TrialBalanceOPT" Title="Untitled Page" %>

<%@ Register src="../Controls/TrialBalanceReport.ascx" tagname="TrialBalanceReport" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:TrialBalanceReport ID="TrialBalanceReport1" runat="server" />
</asp:Content>

