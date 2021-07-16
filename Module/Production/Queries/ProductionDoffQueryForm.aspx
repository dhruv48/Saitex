<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ProductionDoffQueryForm.aspx.cs" Inherits="Module_Production_Queries_ProductionDoffQueryForm" %>

<%@ Register Src="../Controls/ProductionDoffQueryForm.ascx" TagName="ProductionDoffQueryForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:ProductionDoffQueryForm ID="ProductionDoffQueryForm1" runat="server" />
</asp:Content>
