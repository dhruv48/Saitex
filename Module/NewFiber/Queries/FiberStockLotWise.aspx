<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="FiberStockLotWise.aspx.cs" Inherits="Module_Fiber_Queries_FiberStockLotWise" %>
<%@ Register src="~/Module/Fiber/Controls/FiberStockLotWise.ascx" tagname="FiberStockLotWise" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FiberStockLotWise ID="FiberStockLotWise1" runat="server" />
</asp:Content>

