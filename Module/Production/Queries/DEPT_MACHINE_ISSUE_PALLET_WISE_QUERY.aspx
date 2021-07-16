<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="DEPT_MACHINE_ISSUE_PALLET_WISE_QUERY.aspx.cs" Inherits="Module_Production_Queries_DEPT_MACHINE_ISSUE_PALLET_WISE_QUERY" %>

<%@ Register Src="../Controls/DEPT_MACHINE_ISSUE_PALLET_WISE_QUERY.ascx" TagName="ProductionIssQtyForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:ProductionIssQtyForm ID="ProductionIssQtyForm" runat="server" />
</asp:Content>

