<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" MaintainScrollPositionOnPostback ="true"  AutoEventWireup="true" CodeFile="AttendanceApproving.aspx.cs" Inherits="Module_HRMS_Pages_AttendanceApproving" Title="Untitled Page" %>

<%@ Register src="../Controls/Salary_Pre.ascx" tagname="Salary_Pre" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Salary_Pre ID="Salary_Pre1" runat="server" />
</asp:Content>

