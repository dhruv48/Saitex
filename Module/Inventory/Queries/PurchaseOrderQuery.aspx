﻿<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="PurchaseOrderQuery.aspx.cs" Inherits="Module_Inventory_Queries_PurchaseOrderQuery" Title="Untitled Page" %>

<%@ Register src="PurchaseOrderQuery.ascx" tagname="PurchaseOrderQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:PurchaseOrderQuery ID="PurchaseOrderQuery1" runat="server" />
</asp:Content>
