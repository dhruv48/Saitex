<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="JournalEntryForm.aspx.cs" Inherits="Module_FA_Pages_JournalEntryForm" Title="Untitled Page" %>

<%@ Register src="../Controls/JournalEntry.ascx" tagname="JournalEntry" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:JournalEntry ID="JournalEntry1" runat="server" />
</asp:Content>

