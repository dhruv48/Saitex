<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="CRForWaste.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CRForWaste" %>

<%@ Register Src="~/Module/OrderDevelopment/CustomerRequest/Controls/CRForWaste.ascx" TagName="CRForWaste" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:CRForWaste id="CRForWaste" runat = "server"></uc1:CRForWaste>
</asp:Content>