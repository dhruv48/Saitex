<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SalaryDetails.aspx.cs" Inherits="Module_HRMS_Pages_SalaryDetails" Title="Untitled Page" %>

<%@ Register src="../Controls/SalaryDetails.ascx" tagname="SalaryDetails" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:SalaryDetails ID="SalaryDetails1" runat="server" />
</asp:Content>

