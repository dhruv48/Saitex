<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="RateComponentQuery.aspx.cs" Inherits="Module_Admin_Queries_RateComponentQuery" %>
<%@ Register Src="~/Module/Admin/Controls/Rate_Component.ascx" TagName="Rate_Component" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:Rate_Component id="Rate_Component1" runat="server" /> 
</asp:Content>

