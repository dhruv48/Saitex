<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="PAF.aspx.cs" Inherits="Module_HRMS_Pages_PAF" %>

<%@ Register src="../Controls/PAF.ascx" tagname="PAF" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:PAF ID="PAF1" runat="server" />
</asp:Content>
