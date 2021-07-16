<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="GateIN.aspx.cs" Inherits="Module_GateEntry_Reports_GateIN" Title="Untitled Page" %>
<%@ Register src="~/Module/GateEntry/Controls/GateIn.ascx" tagname="GateIn" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<uc1:GateIn ID="GateIn" runat="server" />

</asp:Content>

