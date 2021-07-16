<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Promotion.aspx.cs" Inherits="Module_HRMS_Pages_Promotion" Title="Untitled Page" %>

<%@ Register src="../Controls/Promotion.ascx" tagname="Promotion" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Promotion ID="Promotion1" runat="server" />
</asp:Content>

