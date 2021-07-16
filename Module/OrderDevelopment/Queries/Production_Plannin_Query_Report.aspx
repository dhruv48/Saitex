<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Production_Plannin_Query_Report.aspx.cs" Inherits="Module_OrderDevelopment_Queries_Production_Plannin_Query_Report" %>

<%@ Register src="../Controls/ProductionPlanningQuery.ascx" tagname="OrderCaptureQuery3" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:OrderCaptureQuery3 ID="OrderCaptureQuery18" runat="server" />
</asp:Content>
