<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fiber_Detail_Report.aspx.cs" Inherits="Module_Fiber_Reports_Fiber_Detail_Report" %>

<%@ Register src="../Controls/Fiber_Detail_Report.ascx" tagname="Fiber_Detail_Report" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Fiber_Detail_Report ID="Fiber_Detail_Report1" runat="server" />
</asp:Content>

