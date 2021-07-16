<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ReadMail.aspx.cs" Inherits="Module_Mail_Pages_ReadMail" Title="Untitled Page" %>

<%@ Register src="../Controls/Inbox.ascx" tagname="Inbox" tagprefix="uc1" %>

<%@ Register src="../Controls/ReadMail.ascx" tagname="ReadMail" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:ReadMail ID="ReadMail1" runat="server" />
</asp:Content>

