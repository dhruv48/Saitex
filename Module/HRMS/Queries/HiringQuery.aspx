<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="HiringQuery.aspx.cs" Inherits="Module_HRMS_Queries_HiringQuery" Title="Untitled Page" %>

<%@ Register src="../Controls/HiringQueryDetail.ascx" tagname="HiringQueryDetail" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:HiringQueryDetail ID="HiringQueryDetail1" runat="server" />
</asp:Content>

