<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Monthly_PF_ESI_REPORT.aspx.cs" Inherits="Module_HRMS_Pages_Monthly_PF_ESI_REPORT" %>

<%@ Register src="../Controls/Monthly_PF_ESI_Report.ascx" tagname="Monthly_PF_ESI_Report" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Monthly_PF_ESI_Report ID="Monthly_PF_ESI_Report1" runat="server" />
</asp:Content>

