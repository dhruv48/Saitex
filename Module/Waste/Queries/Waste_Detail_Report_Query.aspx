﻿<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Waste_Detail_Report_Query.aspx.cs" Inherits="Module_Waste_Queries_Waste_Detail_Report_Query" Title="Untitled Page" %>

<%@ Register src="../Controls/Waste_detail_Report.ascx" tagname="Waste_detail_Report" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Waste_detail_Report ID="Waste_detail_Report1" runat="server" />
</asp:Content>
