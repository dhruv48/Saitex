<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="StaffLeaveApproving.aspx.cs" Inherits="Module_HRMS_Pages_StaffLeaveApproving" %>

<%@ Register src="../Controls/StaffLeaveApproval.ascx" tagname="StaffLeaveApproval" tagprefix="uc1" %>

<%@ Register src="../Controls/StaffLeaveAppr.ascx" tagname="StaffLeaveAppr" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:StaffLeaveAppr ID="StaffLeaveAppr1" runat="server" />
</asp:Content>

