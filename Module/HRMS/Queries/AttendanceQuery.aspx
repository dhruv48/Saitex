<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="AttendanceQuery.aspx.cs" Inherits="Module_HRMS_Queries_AttendanceQuery" Title="Untitled Page" %>

<%@ Register src="../Controls/AttendanceQuery.ascx" tagname="AttendanceQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:AttendanceQuery ID="AttendanceQuery1" runat="server" />
</asp:Content>


