<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Yarn_Indent_Approval.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Yarn_Indent_Approval" Title="Untitled Page" %>

<%@ Register src="../Controls/Indent_Approval.ascx" tagname="Indent_Approval" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Indent_Approval ID="Indent_Approval1" runat="server" />
</asp:Content>

