<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="EmployeeAdvanceHODApproval.aspx.cs" Inherits="Module_HRMS_Pages_EmployeeAdvanceHOD_Approval" Title="Untitled Page" %>



<%@ Register src="../Controls/EmployeeAdvanceHODApproval.ascx" tagname="EmployeeAdvanceHODApproval" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:EmployeeAdvanceHODApproval ID="EmployeeAdvanceHODApproval1" 
        runat="server" />
</asp:Content>

