<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Hiring.aspx.cs" Inherits="Module_HRMS_Pages_Hiring" Title="Untitled Page" %>
<%@ Register src="../Controls/Hiring.ascx" tagname="Hiring" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Hiring ID="Hiring1" runat="server" />
</asp:Content>

