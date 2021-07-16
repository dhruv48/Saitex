<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="EMPLOYEE_EXP_DETAIL.aspx.cs" Inherits="Module_HRMS_Pages_EMPLOYEE_EXP_DETAIL" Title="Untitled Page" %>

<%@ Register src="../Controls/EMPLOYEE_EXP_DETAIL.ascx" tagname="EMPLOYEE_EXP_DETAIL" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:EMPLOYEE_EXP_DETAIL ID="EMPLOYEE_EXP_DETAIL1" runat="server" />
</asp:Content>

