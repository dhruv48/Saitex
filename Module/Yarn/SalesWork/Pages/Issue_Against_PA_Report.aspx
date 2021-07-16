<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Issue_Against_PA_Report.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Issue_Against_PA_Report" Title="Untitled Page" %>
<%@ Register src="~/Module/Yarn/SalesWork/Queries/Issue_Against_PA_Report.ascx" tagname="Issue_Against_PA_Report" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:Issue_Against_PA_Report ID="Issue_Against_PA_Report" runat="server" />
</asp:Content>

