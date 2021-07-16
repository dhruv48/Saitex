<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Payroll_Master.aspx.cs" Inherits="Module_Admin_Pages_Payroll_Master" Title="Untitled Page" %>

<%@ Register src="../Controls/Payroll_MST.ascx" tagname="Payroll_MST" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Payroll_MST ID="Payroll_MST1" runat="server" />
</asp:Content>

