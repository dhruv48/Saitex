﻿<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="DEmoAttendance.aspx.cs" Inherits="Module_HRMS_Pages_DEmoAttendance" Title="Untitled Page" %>

<%@ Register src="../Controls/Attendance_New.ascx" tagname="Attendance_New" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Attendance_New ID="Attendance_New1" runat="server" />
</asp:Content>

