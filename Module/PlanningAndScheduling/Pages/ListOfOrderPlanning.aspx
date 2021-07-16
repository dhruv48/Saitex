<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListOfOrderPlanning.aspx.cs" MasterPageFile="~/CommonMaster/admin.master"  Inherits="Module_PlanningAndScheduling_Pages_ListOfOrderPlanning" %>
<%@ Register src="~/Module/PlanningAndScheduling/Controls/ListOfOrderPlanning.ascx" tagname="OrderMachinePlanningList" tagprefix="uc1" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">

<uc1:OrderMachinePlanningList ID="OMPList" runat="server" />
                    
</asp:Content>
