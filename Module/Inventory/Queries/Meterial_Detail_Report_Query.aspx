<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Meterial_Detail_Report_Query.aspx.cs" Inherits="Module_Inventory_Queries_Meterial_Detail_Report_Query" Title="Untitled Page" %>

<%@ Register src="../Controls/Meterial_detail_Report.ascx" tagname="Meterial_detail_Report" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Meterial_detail_Report ID="Meterial_detail_Report1" runat="server" />
</asp:Content>

