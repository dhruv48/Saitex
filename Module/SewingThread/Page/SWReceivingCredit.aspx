<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SWReceivingCredit.aspx.cs" Inherits="Module_Sewing_Thread_Page_SWReceivingCredit" Title="Untitled Page" %>

<%@ Register src="../Controls/SWReceivingCredit.ascx" tagname="SWReceivingCredit" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:SWReceivingCredit ID="SWReceivingCredit1" runat="server" />
</asp:Content>

