<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="BillEntryYarn.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_BillEntryYarn" Title="Untitled Page" %>

<%@ Register src="../Controls/BillEntryYarn.ascx" tagname="BillEntryYarn" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:BillEntryYarn ID="BillEntryYarn1" runat="server" />
</asp:Content>

