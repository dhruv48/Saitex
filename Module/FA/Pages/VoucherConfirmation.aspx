<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="VoucherConfirmation.aspx.cs" Inherits="Module_FA_Pages_VoucherConfirmation" Title="Untitled Page" %>

<%@ Register src="../Controls/VoucherConfirm.ascx" tagname="VoucherConfirm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:VoucherConfirm ID="VoucherConfirm1" runat="server" />
</asp:Content>

