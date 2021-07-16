<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OrderCapturePPC.aspx.cs" Inherits="Module_OrderDevelopment_Pages_OrderCapturePPC" Title="Untitled Page" %>

<%@ Register src="../Controls/PPC.ascx" tagname="PPC" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:PPC ID="PPC1" runat="server" />
</asp:Content>

