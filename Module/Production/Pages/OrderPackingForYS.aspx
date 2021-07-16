<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OrderPackingForYS.aspx.cs" Inherits="Module_Production_Pages_OrderPackingForYS" %>
<%@ Register Src="~/Module/Production/Controls/OrderPacking4YS1.ascx"  TagName="OrderPacking4YS1" TagPrefix="uc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:OrderPacking4YS1 ID="OrderPackingForYS1" runat="server" />
</asp:Content>

