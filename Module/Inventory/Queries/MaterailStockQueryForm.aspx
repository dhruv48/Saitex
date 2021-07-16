<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MaterailStockQueryForm.aspx.cs" Inherits="Module_Inventory_Queries_MaterailStockQueryForm" Title="Untitled Page" %>

<%@ Register src="../Controls/MaterailStockQueryForm.ascx" tagname="MaterailStockQueryForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:MaterailStockQueryForm ID="MaterailStockQueryForm1" runat="server" />
</asp:Content>

