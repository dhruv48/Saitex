<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="vouchermasterreport.aspx.cs" Inherits="Module_FA_Reports_vouchermasterreport" Title="Untitled Page" %>

<%@ Register src="../Controls/vouchermasterreport.ascx" tagname="vouchermasterreport" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:vouchermasterreport ID="vouchermasterreport1" runat="server" />
</asp:Content>

