<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="GateOut.aspx.cs" Inherits="Module_GateEntry_Pages_GateOut" %>

<%@ Register src="../Controls/GateOut.ascx" tagname="GateOut" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:GateOut ID="GateOut1" runat="server" />
</asp:Content>

