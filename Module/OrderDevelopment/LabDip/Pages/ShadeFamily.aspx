﻿<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="ShadeFamily.aspx.cs" Inherits="Module_OrderDevelopment_LabDip_Pages_ShadeFamily"
    Title="Untitled Page" %>

<%@ Register Src="~/CommonControls/Transaction_Of_Master.ascx" TagName="Transaction_Of_Master"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:Transaction_Of_Master ID="Transaction_Of_Master1" runat="server" />
</asp:Content>
