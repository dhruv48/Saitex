﻿<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ReceiptApproval.aspx.cs" Inherits="Module_Inventory_Pages_ReceiptApproval" Title="Untitled Page" %>

<%@ Register src="../Controls/ReceiptApproval.ascx" tagname="ReceiptApproval" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ReceiptApproval ID="ReceiptApproval1" runat="server" />
</asp:Content>

