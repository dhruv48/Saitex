<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="AppointmentLetter.aspx.cs" Inherits="Module_HRMS_Pages_AppointmentLetter" Title="Untitled Page" %>

<%@ Register src="../Controls/AppointmentLetter.ascx" tagname="AppointmentLetter" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:AppointmentLetter ID="AppointmentLetter1" runat="server" />
</asp:Content>

