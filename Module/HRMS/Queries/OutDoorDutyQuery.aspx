<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OutDoorDutyQuery.aspx.cs" Inherits="Module_HRMS_Queries_OutDoorDutyQuery" Title="Untitled Page" %>

<%@ Register src="../Controls/OutDoorDutyQuery.ascx" tagname="OutDoorDutyQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:OutDoorDutyQuery ID="OutDoorDutyQuery1" runat="server" />
</asp:Content>

