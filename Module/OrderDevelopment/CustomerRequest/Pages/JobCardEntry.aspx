<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="JobCardEntry.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_JobCardEntry" %>
<%@ Register Src="~/Module/OrderDevelopment/CustomerRequest/Controls/JobCardEntry_Dyeing.ascx" TagName="JobCardEntry_Dyeing" TagPrefix="uc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:JobCardEntry_Dyeing ID="JobCardEntry_Dyeing" runat="server" />
</asp:Content>


