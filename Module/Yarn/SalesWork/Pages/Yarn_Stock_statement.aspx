<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Yarn_Stock_statement.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Yarn_Stock_statement" Title="Untitled Page" %>

<%@ Register src="../Controls/Yarn_Stock_Statement.ascx" tagname="Yarn_Stock_Statement" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Yarn_Stock_Statement ID="Yarn_Stock_Statement1" runat="server" />
</asp:Content>

