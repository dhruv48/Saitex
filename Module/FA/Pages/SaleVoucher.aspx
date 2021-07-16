<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SaleVoucher.aspx.cs" Inherits="Module_FA_Pages_SaleVoucher" Title="Untitled Page" %>

<%@ Register src="../Controls/SaleEntry.ascx" tagname="SaleEntry" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:SaleEntry ID="SaleEntry1" runat="server" />
</asp:Content>

