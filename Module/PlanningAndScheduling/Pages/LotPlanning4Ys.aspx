<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LotPlanning4Ys.aspx.cs" Inherits="Module_PlanningAndScheduling_Pages_LotPlanning4Ys" %>

<%@ Register src="../Controls/LotPlanning4YS.ascx" tagname="LotPlanning4YS" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:LotPlanning4YS ID="LotPlanning4YS1" runat="server" />
</asp:Content>

