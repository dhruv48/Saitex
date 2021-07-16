<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OrderFlowDetail4YD.aspx.cs" Inherits="Module_PlanningAndScheduling_Reports_OrderFlowDetail4YD" %>
<%@ Register src="~/Module/PlanningAndScheduling/Controls/OrderFlowDetail4YD1.ascx" tagname="OrderFlowDetail4YD1" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:OrderFlowDetail4YD1 ID="OrderFlowDetail4YD2" runat="server" />
</asp:Content>

