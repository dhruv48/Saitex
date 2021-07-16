<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Yarn_Reciving.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Yarn_Reciving" Title="Untitled Page" %>

<%@ Register src="../Controls/ReceiptCredit.ascx" tagname="ReceiptCredit" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ReceiptCredit ID="ReceiptCredit1" runat="server" />
</asp:Content>

