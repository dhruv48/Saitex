﻿<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Employee_Advance_Request.aspx.cs" Inherits="Module_HRMS_Pages_Employee_Advance_Request" Title="Untitled Page" %>

<%@ Register src="../Controls/EmployeeAdvanceEmplogin.ascx" tagname="EmployeeAdvanceEmplogin" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
   
    <uc1:EmployeeAdvanceEmplogin ID="EmployeeAdvanceEmplogin1" runat="server" />
   
</asp:Content>

