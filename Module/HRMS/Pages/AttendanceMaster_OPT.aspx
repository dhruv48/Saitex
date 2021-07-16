<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="AttendanceMaster_OPT.aspx.cs" Inherits="Module_HRMS_Pages_AttendanceMaster_OPT" Title="Untitled Page" %>

<%@ Register src="../Controls/AttendanceOPT.ascx" tagname="AttendanceOPT" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

    <uc1:AttendanceOPT ID="AttendanceOPT1" runat="server" />

</asp:Content>

