<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISE.aspx.cs" Inherits="Module_Production_Queries_MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISE" %>

<%@ Register Src="../Controls/MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISE.ascx" TagName="Mach_IssDetail_COPS" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:Mach_IssDetail_COPS ID="Mach_IssDetail_COPS" runat="server" />
</asp:Content>