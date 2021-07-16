<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="Gate_Entry_query.aspx.cs" Inherits="Module_GateEntry_NewFolder1_Gate_Entry_query" %>

<%@ Register src="../Controls/Gate_Entry_detail.ascx" tagname="Gate_Entry_detail" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:Gate_Entry_detail ID="Gate_Entry_detail1" runat="server" />
</asp:Content>
