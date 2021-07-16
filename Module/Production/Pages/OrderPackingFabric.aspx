<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OrderPackingFabric.aspx.cs" Inherits="Module_Production_Pages_OrderPackingFabric" %>

<%@ Register src="~/Module/Production/Controls/OrderPackFabNew.ascx" tagname="OrderPackingFabric" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:OrderPackingFabric ID="OrderPackingFabric1" runat="server" />
</asp:Content>

