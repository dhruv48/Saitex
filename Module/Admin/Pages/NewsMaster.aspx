<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="NewsMaster.aspx.cs" Inherits="Module_Admin_Pages_NewsMaster" Title="Untitled Page" ValidateRequest ="false"  %>

<%@ Register src="../Controls/News_Master.ascx" tagname="News_Master" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:News_Master ID="News_Master1" runat="server" />
</asp:Content>

