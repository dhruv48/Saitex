<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CRForYarnQuery.aspx.cs" Inherits="Module_Yarn_SalesWork_Queries_CRForYarnQuery" %>
<%@ Register Src="~/Module/Yarn/SalesWork/Controls/CRQueryYarn.ascx" TagName="CRQueryForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:CRQueryForm ID="CRQueryForm1" runat="server" />
</asp:Content>

