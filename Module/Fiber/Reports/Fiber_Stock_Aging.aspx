<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fiber_Stock_Aging.aspx.cs" Inherits="Module_Fiber_Reports_Fiber_Stock_Aging" %>

<%@ Register src="../Controls/FiberStockAgeing_Opt.ascx" tagname="FiberStockAgeing_Opt" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FiberStockAgeing_Opt ID="FiberStockAgeing_Opt" runat="server" />
</asp:Content>

