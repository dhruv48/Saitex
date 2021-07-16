<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Vat_Retail_Report.aspx.cs" Inherits="Module_Inventory_Pages_Vat_Retail_Report" Title="Untitled Page" %>

<%@ Register src="../Controls/Vat_Retail_Price.ascx" tagname="Vat_Retail_Price" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Vat_Retail_Price ID="Vat_Retail_Price1" runat="server" />
</asp:Content>

