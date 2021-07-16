<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Telephonebill.aspx.cs" Inherits="Module_HRMS_Pages_Telephonebill" Title="Untitled Page" %>

<%@ Register src="../Controls/Telephonebill.ascx" tagname="Telephonebill" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Telephonebill ID="Telephonebill1" runat="server" />
</asp:Content>

