<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MonthlyAbsentReport.aspx.cs" Inherits="Module_HRMS_Pages_MonthlyAbsentReport" %>

<%@ Register src="../Controls/Monthly_Absent_Report.ascx" tagname="Monthly_Absent_Report" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Monthly_Absent_Report ID="Monthly_Absent_Report1" runat="server" />
</asp:Content>

