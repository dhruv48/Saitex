<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Production_Planning_Query.aspx.cs" Inherits="Module_OrderDevelopment_Queries_Production_Planning_Query" Title="Untitled Page" %>
<%@ Register src="~/Module/OrderDevelopment/Controls/Production_Planning_Query.ascx" tagname="Production_Planning_Query" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:Production_Planning_Query ID="Production_Planning_Query" runat="server" />
</asp:Content>

