<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="OutBox.aspx.cs" Inherits="Module_Mail_Pages_OutBox" Title="Untitled Page" %>

<%@ Register Src="../Controls/SentMail.ascx" TagName="SentMail" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:SentMail ID="SentMail1" runat="server" />
</asp:Content>
