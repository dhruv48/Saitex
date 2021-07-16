<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ProductionEntry.aspx.cs" Inherits="Module_Production_Pages_ProductionEntry" Title="Untitled Page" %>

<%@ Register src="../Controls/ProductionEntry.ascx" tagname="ProductionEntry" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ProductionEntry ID="ProductionEntry1" runat="server" />
</asp:Content>

