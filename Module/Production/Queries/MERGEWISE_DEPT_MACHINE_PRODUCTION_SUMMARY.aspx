<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MERGEWISE_DEPT_MACHINE_PRODUCTION_SUMMARY.aspx.cs" Inherits="Module_Production_Queries_MERGEWISE_DEPT_MACHINE_PRODUCTION_SUMMARY" %>

<%@ Register Src="../Controls/MERGEWISE_DEPT_MACHINE_PRODUCTION_SUMMARY.ascx" TagName="WIPStockQueryForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:WIPStockQueryForm ID="WIPStockQueryForm1" runat="server" />
</asp:Content>
