<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="Budget.aspx.cs" Inherits="Module_HRMS_Pages_Budget" Title="DepartMent Budget" %>

<%@ Register Src="../Controls/DEPT_BUDGET.ascx" TagName="DEPT_BUDGET" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:DEPT_BUDGET ID="DEPT_BUDGET1" runat="server" />
</asp:Content>
