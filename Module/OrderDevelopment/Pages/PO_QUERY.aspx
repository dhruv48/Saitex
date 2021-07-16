<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="PO_QUERY.aspx.cs" Inherits="Module_OrderDevelopment_Pages_PO_QUERY" Title="Untitled Page" %>

<%@ Register src="../Controls/PO_QUERY.ascx" tagname="PO_QUERY" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:PO_QUERY ID="PO_QUERY1" runat="server" />
</asp:Content>

