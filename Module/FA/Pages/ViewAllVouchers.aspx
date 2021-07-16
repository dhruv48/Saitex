<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ViewAllVouchers.aspx.cs" Inherits="Module_FA_Pages_ViewAllVouchers" Title="Untitled Page" %>

<%@ Register src="../Controls/ViewAllVouchers.ascx" tagname="ViewAllVouchers" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ViewAllVouchers ID="ViewAllVouchers1" runat="server" />
</asp:Content>

