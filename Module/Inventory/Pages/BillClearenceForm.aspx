﻿<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="BillClearenceForm.aspx.cs" Inherits="Module_Inventory_Pages_BillClearenceForm" Title="Untitled Page" %>


<%@ Register src="../Controls/BillClearenceForm1.ascx" tagname="BillClearenceForm1" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:BillClearenceForm1 ID="BillClearenceForm11" runat="server" />
</asp:Content>

