<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="TrialBalance.aspx.cs" Inherits="Module_FA_Pages_TrialBalance" Title="Untitled Page" %>

<%@ Register src="../Controls/TrailBalance.ascx" tagname="TrailBalance" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:TrailBalance ID="TrailBalance1" runat="server" />
</asp:Content>

