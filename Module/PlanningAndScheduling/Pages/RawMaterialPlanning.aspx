<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="RawMaterialPlanning.aspx.cs" Inherits="Module_PlanningAndScheduling_Pages_RawMaterialPlanning" %>
<%@ Register Src="~/Module/PlanningAndScheduling/Controls/RawMaterialPlan.ascx"  TagName="LotPlanning" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:LotPlanning ID="LotPlanning1" runat="server" />
</asp:Content>

