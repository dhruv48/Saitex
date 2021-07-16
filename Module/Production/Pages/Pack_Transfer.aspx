<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/CommonMaster/admin.master"
    AutoEventWireup="true" CodeFile="Pack_Transfer.aspx.cs" Inherits="Module_Production_Pages_Pack_Transfer" %>

<%@ Register Src="../Controls/Pack_trans.ascx" TagName="Pack_trans" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:Pack_trans ID="Pack_trans1" runat="server" />
</asp:Content>
