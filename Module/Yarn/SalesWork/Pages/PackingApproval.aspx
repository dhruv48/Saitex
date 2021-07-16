<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="PackingApproval.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_PackingApproval" %>
<%@ Register src="../Controls/PackingApproval.ascx" tagname="PackingApproval" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:PackingApproval ID="PackingApproval" runat="server" />
</asp:Content>



