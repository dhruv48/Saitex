<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FiberPOClosing.aspx.cs" Inherits="Module_Fiber_Pages_FabricPOClosing" Title="Untitled Page" %>

<%@ Register src="../Controls/FiberPOClosing.ascx" tagname="PO_Approval" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:PO_Approval ID="PO_Approval1" runat="server" />
</asp:Content>

