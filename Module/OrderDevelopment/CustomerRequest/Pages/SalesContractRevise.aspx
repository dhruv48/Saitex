<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master"  AutoEventWireup="true" CodeFile="SalesContractRevise.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_SalesContractRevise" %>
<%@ Register src="~/Module/OrderDevelopment/CustomerRequest/Controls/SalesContractRevise.ascx" tagname="SalesContractRevise" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:SalesContractRevise ID="SalesContractRevise1" runat="server" />
</asp:Content>
