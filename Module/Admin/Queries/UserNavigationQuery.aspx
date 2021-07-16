<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="UserNavigationQuery.aspx.cs" Inherits="Module_Admin_Queries_UserNavvigationQuery" %>
<%@ Register Src="~/Module/Admin/Controls/User_Navigation.ascx" TagName="User_Navigation" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:User_Navigation ID="User_Navigation1" runat="server" />
</asp:Content>

