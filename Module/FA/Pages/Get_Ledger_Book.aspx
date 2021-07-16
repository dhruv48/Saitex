<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Get_Ledger_Book.aspx.cs" Inherits="Module_FA_Pages_Get_Ledger_Book" Title="Untitled Page" %>

<%@ Register src="../Controls/Ledger_Book.ascx" tagname="Ledger_Book" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Ledger_Book ID="Ledger_Book1" runat="server" />
</asp:Content>

