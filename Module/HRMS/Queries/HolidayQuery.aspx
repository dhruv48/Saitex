<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="HolidayQuery.aspx.cs" Inherits="Module_HRMS_Queries_HolidayQuery" Title="Untitled Page" %>

<%@ Register src="../Controls/HolidayQuery.ascx" tagname="HolidayQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:HolidayQuery ID="HolidayQuery1" runat="server" />
</asp:Content>

