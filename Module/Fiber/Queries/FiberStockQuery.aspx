<%@ Page Language="C#"  MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FiberStockQuery.aspx.cs" Inherits="Module_NewFiber_Queries_FiberStockQuery" %>

<%@ Register src="../Controls/FiberStockQueryForm.ascx" tagname="FiberStockQueryForm" tagprefix="uc1" %>
<%--<%@ Register Src="~/Module/NewFiber/Controls/FiberStockQueryForm.ascx" TagPrefix="uc2" TagName="FiberStockQueryForm" %>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
    <script type="text/javascript" language="javascript" src="../../../../javascript/jquery.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../../../javascript/jquery.colorbox.js"></script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FiberStockQueryForm ID="FiberStockQueryForm1" runat="server" />
</asp:Content>

