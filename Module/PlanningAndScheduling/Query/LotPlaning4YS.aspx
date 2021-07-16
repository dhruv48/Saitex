<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LotPlaning4YS.aspx.cs" Inherits="Module_PlanningAndScheduling_Query_LotPlaning4YS" %>

<%@ Register src="../Controls/LotPlaningQueryForm.ascx" tagname="LotPlaningQueryForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:LotPlaningQueryForm ID="LotPlaningQueryForm1" runat="server" />
</asp:Content>

