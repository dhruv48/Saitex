<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master"  AutoEventWireup="true" CodeFile="FiberIssue.aspx.cs" Inherits="Module_Fiber_Queries_FiberIssue" %>

<%@ Register src="../Controls/FiberIssue.ascx" tagname="FiberIssue" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FiberIssue ID="FiberIssue1" runat="server" />
</asp:Content>

