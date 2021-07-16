<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Monthly_Attn_IN_Out_Rpt.aspx.cs" Inherits="Module_HRMS_Pages_Monthly_Attn_IN_Out_Rpt" Title="Untitled Page" %>

<%@ Register src="../Controls/Monthly_Attendance_Rpt.ascx" tagname="Monthly_Attendance_Rpt" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Monthly_Attendance_Rpt ID="Monthly_Attendance_Rpt1" runat="server" />
</asp:Content>

