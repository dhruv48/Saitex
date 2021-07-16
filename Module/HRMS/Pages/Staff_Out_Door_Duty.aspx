<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Staff_Out_Door_Duty.aspx.cs" Inherits="Module_HRMS_Pages_Staff_Out_Door_Duty" %>

<%@ Register src="../Controls/Staff_Out_Door_Duty.ascx" tagname="Staff_Out_Door_Duty" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Staff_Out_Door_Duty ID="Staff_Out_Door_Duty1" runat="server" />
</asp:Content>

