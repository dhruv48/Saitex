<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="WO_Issue_Approval.aspx.cs" Inherits="Module_WorkOrder_Pages_WO_Issue_Approval" %>

<%@ Register src="../Controls/WO_Issue_Approval.ascx" tagname="WO_Issue_Approval" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:WO_Issue_Approval ID="WO_Issue_Approval" runat="server" />
</asp:Content>


