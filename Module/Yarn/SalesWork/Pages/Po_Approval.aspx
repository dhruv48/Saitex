<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Po_Approval.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Po_Approval" Title="Untitled Page" %>

<%@ Register src="../Controls/POApproval.ascx" tagname="POApproval" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:POApproval ID="POApproval1" runat="server" />
</asp:Content>

