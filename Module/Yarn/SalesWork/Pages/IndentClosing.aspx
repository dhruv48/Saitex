<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="IndentClosing.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_IndentClosing" Title="Untitled Page" %>

<%@ Register src="../Controls/IndentClosing.ascx" tagname="IndentClosing" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:IndentClosing ID="IndentClosing1" runat="server" />
</asp:Content>

