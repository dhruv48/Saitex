<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SWPOCredit.aspx.cs" Inherits="Module_Sewing_Thread_Page_SWPOCredit" Title="Untitled Page" %>

<%@ Register src="../Controls/SWPOCredit.ascx" tagname="SWPOCredit" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:SWPOCredit ID="SWPOCredit1" runat="server" />
</asp:Content>

