<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Yarn_PoCredit.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Yarn_PoCredit" Title="Untitled Page" %>

<%@ Register src="../Controls/POCredit.ascx" tagname="POCredit" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:POCredit ID="POCredit1" runat="server" />
</asp:Content>

