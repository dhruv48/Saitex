<%@ Page Title="" Language="C#" EnableEventValidation="false"   MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Order_Caputre_Query.aspx.cs" Inherits="Module_OrderDevelopment_Queries_Order_Caputre_Query" %>

<%@ Register src="OrderCaptureQuery.ascx" tagname="OrderCaptureQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:OrderCaptureQuery ID="OrderCaptureQuery1" runat="server" />
</asp:Content>

