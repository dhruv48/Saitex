<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FiberStockReport.aspx.cs" Inherits="Module_Fiber_Reports_FiberStockReport" %>

<%@ Register src="../Controls/FiberStockQueryForm.ascx" tagname="FiberStockQueryForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
    <script type="text/javascript" language="javascript" src="../../../../javascript/jquery.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../../../javascript/jquery.colorbox.js"></script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FiberStockQueryForm ID="FiberStockQueryForm1" runat="server" />
</asp:Content>

