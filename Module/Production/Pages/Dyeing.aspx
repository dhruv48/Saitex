<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Dyeing.aspx.cs" Inherits="Module_Production_Pages_Dyeing" Title="Untitled Page" %>
<%@ Register src="../Controls/Dyeing.ascx" tagname="Dyeing" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Dyeing ID="Dyeing" runat="server" />
</asp:Content>

