<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="StockAgeingQueryForm.aspx.cs" Inherits="Module_Waste_Queries_StockAgeingQueryForm" %>

<%@ Register src="../Controls/StockAgeingQuery.ascx" tagname="StockAgeingQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:StockAgeingQuery ID="StockAgeingQuery1" runat="server" />
</asp:Content>

