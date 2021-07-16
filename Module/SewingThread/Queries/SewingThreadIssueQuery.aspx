<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SewingThreadIssueQuery.aspx.cs" Inherits="Module_SewingThread_Queries_SewingThreadIssueQuery" %>

<%@ Register src="../Controls/SewingIssueQuery.ascx" tagname="SewingIssueQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:SewingIssueQuery ID="SewingIssueQuery1" runat="server" />
</asp:Content>

