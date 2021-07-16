<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="PATraining.aspx.cs" Inherits="Module_HRMS_Pages_PATraining" %>

<%@ Register src="../Controls/PATraining.ascx" tagname="PATraining" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:PATraining ID="PATraining1" runat="server" />
</asp:Content>
