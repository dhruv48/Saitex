<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Yarn_Po_Closing.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Yarn_Po_Closing" Title="Untitled Page" %>

<%@ Register src="../Controls/POClosing.ascx" tagname="POClosing" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:POClosing ID="POClosing1" runat="server" />
</asp:Content>

