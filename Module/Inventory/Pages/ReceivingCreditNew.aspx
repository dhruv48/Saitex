﻿<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ReceivingCreditNew.aspx.cs" Inherits="Module_Inventory_Pages_ReceivingCreditNew" Title="Untitled Page" %>

<%@ Register src="../Controls/ReceivingCredit_new.ascx" tagname="ReceivingCredit_new" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ReceivingCredit_new ID="ReceivingCredit_new1" runat="server" />
</asp:Content>

