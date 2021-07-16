<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Department_Wise_Salary.aspx.cs" Inherits="Module_HRMS_Pages_Department_Wise_Salary" %>

<%@ Register src="../Controls/Department_Salary_Rpt.ascx" tagname="Department_Salary_Rpt" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Department_Salary_Rpt ID="Department_Salary_Rpt1" runat="server" />
</asp:Content>

