<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CRForSTReportOPT.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Reports_CRForSTReportOPT" Title="Untitled Page" %>

<%@ Register src="../Controls/CRForSTReportOPT.ascx" tagname="CRForSTReportOPT" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:CRForSTReportOPT ID="CRForSTReportOPT1" runat="server" />
</asp:Content>

