<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ItemReceiptAgainstWO.aspx.cs" Inherits="WorkOrder_ItemReceiptAgainstWO" Title="Untitled Page" %>

<%@ Register src="~/Module/WorkOrder/Controls/ItemReceivingAgainstWO.ascx" tagname="ReceiptHeading" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:ReceiptHeading ID="ReceiptHeading1" runat="server" />
</asp:Content>

