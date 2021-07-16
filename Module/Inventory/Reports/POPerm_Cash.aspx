<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="POPerm_Cash.aspx.cs" Inherits="Module_Inventory_Reports_POPerm_Cash" Title="Untitled Page" %>

<%@ Register src="../Controls/POPermrpt.ascx" tagname="POPermrpt" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:POPermrpt ID="POPermrpt1" runat="server" />
</asp:Content>

