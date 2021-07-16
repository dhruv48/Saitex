<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Warp_Entry.aspx.cs" Inherits="Module_Production_Pages_Warp_Entry" %>

<%@ Register src="../Controls/Warp_Entry.ascx" tagname="Warp_Entry" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Warp_Entry ID="Warp_Entry1" runat="server" />
</asp:Content>

