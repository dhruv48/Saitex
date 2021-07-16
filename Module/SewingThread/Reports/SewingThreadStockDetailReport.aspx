<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SewingThreadStockDetailReport.aspx.cs" Inherits="Module_SewingThread_Reports_SewingThreadStockDetailReport" %>

<%@ Register src="../Controls/Sw_Detail_Report.ascx" tagname="Sw_Detail_Report" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Sw_Detail_Report ID="Sw_Detail_Report1" runat="server" />
</asp:Content>

