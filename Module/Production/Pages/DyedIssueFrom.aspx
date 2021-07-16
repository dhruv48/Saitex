<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="DyedIssueFrom.aspx.cs" Inherits="Module_Production_Pages_DyedIssueFrom" %>
<%@ Register src="../Controls/DyedIssueFrom.ascx" tagname="DyedIssueFrom" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:DyedIssueFrom ID="DyedIssueFrom" runat="server" />
</asp:Content>


