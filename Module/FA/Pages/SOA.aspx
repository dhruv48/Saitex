<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SOA.aspx.cs" Inherits="Module_FA_Pages_SOA" Title="Untitled Page" %>

<%@ Register src="../Controls/Statement_Of_Account.ascx" tagname="Statement_Of_Account" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Statement_Of_Account ID="Statement_Of_Account1" runat="server" />
</asp:Content>

