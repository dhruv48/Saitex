<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="YarnGateIn.aspx.cs" Inherits="Module_GateEntry_Pages_YarnGateIn" Title="Untitled Page" %>


<%@ Register src="../Controls/YarnGateEntry.ascx" tagname="YarnGateEntry" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:YarnGateEntry ID="YarnGateEntry1" runat="server" />
</asp:Content>

