<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master"  AutoEventWireup="true" CodeFile="CRApprovalForYarnSpinning.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CRApprovalForYarnSpinning" %>
<%@ Register src="~/Module/OrderDevelopment/Yarn_Spining/Controls/CRApprovalforYarnSpinning.ascx" tagname="CRApprovalforYarnSpinning" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:CRApprovalforYarnSpinning id="CRApprovalforYarnSpinning1" runat = "server"></uc1:CRApprovalforYarnSpinning>
</asp:Content>

