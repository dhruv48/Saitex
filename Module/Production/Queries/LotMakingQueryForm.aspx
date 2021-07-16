<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LotMakingQueryForm.aspx.cs" Inherits="Module_Production_Queries_LotMakingQueryForm" %>

<%@ Register src="../Controls/Lot_Making_QueryForm.ascx" tagname="Lot_Making_QueryForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Lot_Making_QueryForm ID="Lot_Making_QueryForm1" runat="server" />
</asp:Content>
