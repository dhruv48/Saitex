<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="POQuery.aspx.cs" Inherits="Module_Inventory_Pages_POQuery" Title="Untitled Page" %>

<%@ Register src="../Controls/POCashQuery.ascx" tagname="POCashQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:POCashQuery ID="POCashQuery1" runat="server" />
</asp:Content>

