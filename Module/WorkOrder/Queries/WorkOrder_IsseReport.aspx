<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="WorkOrder_IsseReport.aspx.cs" Inherits="Module_Yarn_SalesWork_Queries_WorkOrder_IsseReport" %>
<%@ Register src="~/Module/WorkOrder/Controls/WorkOrder_Issue.ascx" tagname="Work_Order_query" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
     <uc1:Work_Order_query ID="Work_Order_query1" runat="server" />
</asp:Content>

