<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="HiringReqApp.aspx.cs" Inherits="Module_HRMS_Pages_HiringReqApp" Title="Untitled Page" %>

<%@ Register src="../Controls/HiringReqApp.ascx" tagname="HiringReqApp" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:HiringReqApp ID="HiringReqApp1" runat="server" />
</asp:Content>

