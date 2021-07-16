<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LedgerBook_OPT.aspx.cs" Inherits="Module_FA_Pages_LedgerBook_OPT" Title="Untitled Page" %>

<%@ Register src="../Controls/LedgerBook_OPT.ascx" tagname="LedgerBook_OPT" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:LedgerBook_OPT ID="LedgerBook_OPT1" runat="server" />
</asp:Content>

