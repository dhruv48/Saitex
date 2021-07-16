<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SWStockTransferReceive.aspx.cs" Inherits="Module_Sewing_Thread_Page_SWStockTransferReceive" Title="Untitled Page" %>

<%@ Register src="../Controls/StockTransferReceive.ascx" tagname="StockTransferReceive" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:StockTransferReceive ID="StockTransferReceive1" runat="server" />
</asp:Content>

