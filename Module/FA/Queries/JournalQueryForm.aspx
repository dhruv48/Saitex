<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="JournalQueryForm.aspx.cs" Inherits="Module_FA_Queries_BankMasterQueryForm" Title="Untitled Page" %>

<%@ Register src="../Controls/BankMasterQueryForm.ascx" tagname="BankMasterQueryForm" tagprefix="uc1" %>

<%@ Register src="../Controls/JournalQueryForm.ascx" tagname="JournalQueryForm" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:JournalQueryForm ID="JournalQueryForm1" runat="server" />
</asp:Content>

