﻿<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MaterialIndentClosing.aspx.cs" Inherits="Module_Inventory_Pages_MaterialIndentClosing" Title="Untitled Page" %>

<%@ Register src="../Controls/MaterialClosing.ascx" tagname="MaterialClosing" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:MaterialClosing ID="MaterialClosing1" runat="server" />
</asp:Content>

