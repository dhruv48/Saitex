<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Receipt_UnApproval.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Receipt_UnApproval" Title="Untitled Page" %>

<%@ Register src="../Controls/Yarn_Receipt_UnApproval.ascx" tagname="Yarn_Receipt_Approval" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Yarn_Receipt_Approval ID="Yarn_Receipt_Approval1" runat="server" />
</asp:Content>

