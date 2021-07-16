<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="TaxIntegration.aspx.cs" Inherits="Module_FA_Pages_TaxIntegration" Title="Untitled Page" %>

<%@ Register src="../Controls/TaxInt.ascx" tagname="TaxInt" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:TaxInt ID="TaxInt1" runat="server" />
</asp:Content>

