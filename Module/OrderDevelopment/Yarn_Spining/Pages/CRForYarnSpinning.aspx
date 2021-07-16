<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CRForYarnSpinning.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CRForYarnSpinning" %>
<%@ Register Src="~/Module/OrderDevelopment/Yarn_Spining/Controls/CRForYarnSpining.ascx" TagName="CRForYarnSpining" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:CRForYarnSpining id="CRForYarnSpining1" runat = "server"></uc1:CRForYarnSpining>
</asp:Content>

