<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CrApprovalFabric.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CrApprovalFabric" %>

<%@ Register src="../Controls/CrFabricApproval.ascx" tagname="CrFabricApproval" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:CrFabricApproval ID="CrFabricApproval1" runat="server" />
</asp:Content>

