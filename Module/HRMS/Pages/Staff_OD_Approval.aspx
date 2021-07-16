<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="Staff_OD_Approval.aspx.cs" Inherits="Module_HRMS_Pages_Staff_OD_Approval" %>

<%@ Register Src="../Controls/Staff_OD_Approval.ascx" TagName="soa"
    TagPrefix="uc1" %>
<%@ Register src="../Controls/StaffOdAppr.ascx" tagname="StaffOdAppr" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc2:StaffOdAppr ID="StaffOdAppr1" runat="server" />
</asp:Content>
