<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Module_Mail_Pages_Test"
    Title="Untitled Page" %>

<%@ Register Src="../Controls/Compose.ascx" TagName="Compose" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:Compose ID="Compose1" runat="server" />
</asp:Content>
