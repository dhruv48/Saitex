<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="CashReceiptsVoucherEntry.aspx.cs" Inherits="Module_FA_Pages_CashReceiptsVoucherEntry"
    Title="Untitled Page" %>

<%@ Register Src="~/Module/FA/Controls/CashReceiptVoucher.ascx" TagName="ReceiptVoucher"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:ReceiptVoucher ID="ReceiptVoucher1" runat="server" />
</asp:Content>
