<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LotPlanning4Yd.aspx.cs" Inherits="Module_PlanningAndScheduling_Reports_LotPlanning4Yd" %>

<%@ Register src="../Controls/LotPlaning4YDReport.ascx" tagname="LotPlaning4YDReport" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:LotPlaning4YDReport ID="LotPlaning4YDReport1" runat="server" />
</asp:Content>

