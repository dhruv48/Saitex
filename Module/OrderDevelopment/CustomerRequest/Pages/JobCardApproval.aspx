<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="JobCardApproval.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_JobCardApproval" %>
<%@ Register Src="../Controls/JobCardApproval.ascx" TagName="JobCardApproval" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Head1">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphBody">
<uc1:JobCardApproval ID="JobCardApproval" runat="server" />
</asp:Content>


