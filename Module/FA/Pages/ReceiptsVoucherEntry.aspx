<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ReceiptsVoucherEntry.aspx.cs" Inherits="Module_FA_Pages_ReceiptsVoucherEntry" Title="Untitled Page" %>
<%@ Register src="../Controls/ReceiptVoucher.ascx" tagname="ReceiptVoucher" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ReceiptVoucher ID="ReceiptVoucher1" runat="server" />
    </asp:Content>

