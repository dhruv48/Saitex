﻿<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="StockTransfersIssueSW.aspx.cs" Inherits="Module_Sewing_Thread_Page_StockTransfersIssueSW" Title="Untitled Page" %>

<%@ Register src="../Controls/StockTransferIssueSW.ascx" tagname="StockTransferIssueSW" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:StockTransferIssueSW ID="StockTransferIssueSW1" runat="server" />
</asp:Content>

