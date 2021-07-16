<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="JournalVoucherReport.aspx.cs" Inherits="Module_FA_Pages_JournalVoucherReport" Title="Untitled Page" %>

<%@ Register src="../Controls/JournalVoucherReport.ascx" tagname="JournalVoucherReport" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:JournalVoucherReport ID="JournalVoucherReport1" runat="server" />
    </asp:Content>

