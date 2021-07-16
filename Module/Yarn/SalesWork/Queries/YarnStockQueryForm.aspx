<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="YarnStockQueryForm.aspx.cs" Inherits="Module_Yarn_SalesWork_Queries_YarnStockQueryForm" Title="Untitled Page" %>

<%@ Register src="../Controls/YarnStockQueryForm.ascx" tagname="YarnStockQueryForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
    <script type="text/javascript" language="javascript" src="../../../../javascript/jquery.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../../../javascript/jquery.colorbox.js"></script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:YarnStockQueryForm ID="YarnStockQueryForm1" runat="server" />
</asp:Content>

