<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="TrialBalanceWithFilter.aspx.cs" Inherits="Module_FA_Pages_TrialBalanceWithFilter" Title="Untitled Page" %>

<%@ Register src="../Controls/TrialBalanceWithFilters.ascx" tagname="TrialBalanceWithFilters" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:TrialBalanceWithFilters ID="TrialBalanceWithFilters1" runat="server" />
</asp:Content>

