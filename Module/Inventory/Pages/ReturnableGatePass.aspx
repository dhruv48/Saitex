<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ReturnableGatePass.aspx.cs" Inherits="Module_Inventory_Pages_ReturnableGatePass" %>

<%@ Register src="../Controls/ReturnableGatePass.ascx" tagname="ReturnableGatePass" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ReturnableGatePass ID="ReturnableGatePass1" runat="server" />
</asp:Content>

