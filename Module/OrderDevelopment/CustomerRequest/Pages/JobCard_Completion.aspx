<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="JobCard_Completion.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_JobCard_Completion" %>

<%@ Register Src="../Controls/JobCard_Completion.ascx" TagName="JobCard_Completion" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Head1">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphBody">
<uc1:JobCard_Completion ID="JobCard_Completion" runat="server" />
</asp:Content>
