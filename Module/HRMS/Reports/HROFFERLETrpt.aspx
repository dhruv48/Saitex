<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="HROFFERLETrpt.aspx.cs" Inherits="Module_HRMS_Reports_HROFFERLETrpt" Title="Untitled Page" %>

<%@ Register src="../Controls/HROFFERLET.ascx" tagname="HROFFERLET" tagprefix="uc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="chpHead" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:HROFFERLET ID="HROFFERLET1" runat="server" />
</asp:Content>

