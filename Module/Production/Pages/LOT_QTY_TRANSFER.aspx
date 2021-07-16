<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/CommonMaster/admin.master" CodeFile="LOT_QTY_TRANSFER.aspx.cs" Inherits="Module_Production_Pages_LOT_QTY_TRANSFER" %>

<%@ Register src="~/Module/Production/Controls/LOT_QTY_TRANSFER.ascx" tagname="ReceiptCreditDirect" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ReceiptCreditDirect ID="ReceiptCredit1" runat="server" />
</asp:Content>
