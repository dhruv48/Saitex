<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MonthlyLeaveQuery.aspx.cs" Inherits="Module_HRMS_Pages_MonthlyLeaveQuery" Title="Untitled Page" %>

<%@ Register src="../Controls/MonthlyLeaveQuery.ascx" tagname="MonthlyLeaveQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:MonthlyLeaveQuery ID="MonthlyLeaveQuery1" runat="server" />
</asp:Content>

