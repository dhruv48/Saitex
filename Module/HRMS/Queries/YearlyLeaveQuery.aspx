<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="YearlyLeaveQuery.aspx.cs" Inherits="Module_HRMS_Queries_YearlyLeaveQuery" Title="Untitled Page" %>

<%@ Register src="../Controls/YearlyLeaveQuery.ascx" tagname="YearlyLeaveQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

    <uc1:YearlyLeaveQuery ID="YearlyLeaveQuery1" runat="server" />

</asp:Content>


