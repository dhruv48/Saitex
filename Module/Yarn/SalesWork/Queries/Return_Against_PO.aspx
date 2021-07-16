<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Return_Against_PO.aspx.cs" Inherits="Module_Yarn_SalesWork_Queries_Return_Against_PO" %>

<%@ Register src="../Controls/YarnReturn_Against_PO.ascx" tagname="YarnPOReturn" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
    <script type="text/javascript" language="javascript" src="../../../../javascript/jquery.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../../../javascript/jquery.colorbox.js"></script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:YarnPOReturn ID="Yarn_POReturn" runat="server" />
</asp:Content>
