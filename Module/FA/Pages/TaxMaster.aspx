<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="TaxMaster.aspx.cs" Inherits="Module_FA_Pages_TaxMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/TaxMaster.ascx" tagname="TaxMaster" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:TaxMaster ID="TaxMaster1" runat="server" />
</asp:Content>

