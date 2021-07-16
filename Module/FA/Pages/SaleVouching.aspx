<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SaleVouching.aspx.cs" Inherits="Module_FA_Pages_SaleVouching" Title="Untitled Page" %>

<%@ Register src="../Controls/SaleVoucher.ascx" tagname="SaleVoucher" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:SaleVoucher ID="SaleVoucher1" runat="server" />
</asp:Content>

