<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="JobCardQuery.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Queries_JobCardQuery" %>

<%@ Register Src="../Controls/JobCardQuery.ascx"  tagprefix="uc1" TagName="JobCardQuery" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Head1">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphBody">
<uc1:JobCardQuery ID="JobCardQuery" runat="server" />
</asp:Content>
