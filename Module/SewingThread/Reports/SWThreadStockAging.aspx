<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SWThreadStockAging.aspx.cs" Inherits="Module_SewingThread_Reports_SWThreadStock_Aging" %>

<%@ Register src="../Controls/SWThreadStockAgingReport.ascx" tagname="SWThreadStockAgingReport" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:SWThreadStockAgingReport ID="SWThreadStockAgingReport1" runat="server" />
</asp:Content>

