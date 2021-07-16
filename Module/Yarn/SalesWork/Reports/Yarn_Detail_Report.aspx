<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Yarn_Detail_Report.aspx.cs" Inherits="Module_Yarn_SalesWork_Reports_Yarn_Detail_Report" Title="Untitled Page" %>

<%@ Register src="../Controls/Yarn_Detail_Report.ascx" tagname="Yarn_Detail_Report" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Yarn_Detail_Report ID="Yarn_Detail_Report1" runat="server" />
</asp:Content>

