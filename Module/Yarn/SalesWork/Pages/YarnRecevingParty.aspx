﻿<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="YarnRecevingParty.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_YarnRecevingParty" Title="Untitled Page" %>
<%@ Register src="../Controls/YarnRecepitParty.ascx" tagname="YarnRecepitMisc" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:YarnRecepitMisc ID="YarnRecepitMisc1" runat="server" />
</asp:Content>

