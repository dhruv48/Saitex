<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LabDipSubmission.aspx.cs" Inherits="Module_OrderDevelopment_LabDip_Pages_LabDipSubmission" Title="Untitled Page" %>
<%@ Register src="../Controls/LabDipSubmission.ascx" tagname="LabDipSubmission" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:LabDipSubmission ID="LabDipSubmission1" runat="server" />
</asp:Content>

