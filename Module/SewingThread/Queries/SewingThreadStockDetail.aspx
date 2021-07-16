<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SewingThreadStockDetail.aspx.cs" Inherits="Module_SewingThread_Queries_SewingThreadStockDetail" %>

<%@ Register src="../Controls/SW_Detail_Report.ascx" tagname="SW_Detail_Report" tagprefix="uc1" %>

<%@ Register src="../Controls/SW_Detail.ascx" tagname="SW_Detail" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:SW_Detail ID="SW_Detail1" runat="server" />
</asp:Content>

