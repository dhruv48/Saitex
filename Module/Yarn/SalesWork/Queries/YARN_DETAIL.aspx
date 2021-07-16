<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="YARN_DETAIL.aspx.cs" Inherits="Module_Yarn_SalesWork_Queries_YARN_DETAIL" Title="Untitled Page" %>

<%@ Register src="../Controls/YARN_DETAIL.ascx" tagname="YARN_DETAIL" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:YARN_DETAIL ID="YARN_DETAIL1" runat="server" />
</asp:Content>

