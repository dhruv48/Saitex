<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="PRODUCTION_ISSUE_CONFIMATION.aspx.cs" Inherits="Module_Production_Pages_PRODUCTION_ISSUE_CONFIMATION" %>
<%@ Register src="~/Module/Production/Controls/Production_Issue_Confirmation.ascx" tagname="ProductionIssue" tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
  <uc1:ProductionIssue ID="ProductionIssueConfirmation" runat="server" />
    </asp:Content>
