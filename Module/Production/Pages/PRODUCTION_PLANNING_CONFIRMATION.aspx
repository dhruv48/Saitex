<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master"  CodeFile="PRODUCTION_PLANNING_CONFIRMATION.aspx.cs" Inherits="Module_Production_Pages_PRODUCTION_PLANNING_CONFIRMATION" %>
<%@ Register src="~/Module/Production/Controls/Production_Confirmation.ascx" tagname="ProductionPlanning" tagprefix="uc1" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
<uc1:ProductionPlanning ID="ProductionIssueConfirmation" runat="server" />
                                
</asp:Content>
