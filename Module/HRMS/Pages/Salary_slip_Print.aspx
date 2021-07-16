<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Salary_slip_Print.aspx.cs" Inherits="Module_HRMS_Pages_Salary_slip_Print" Title="Untitled Page" %>

<%@ Register src="../Controls/Salary_Report_Filter.ascx" tagname="Salary_Report_Filter" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Salary_Report_Filter ID="Salary_Report_Filter1" runat="server" />
</asp:Content>

