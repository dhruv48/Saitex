<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ProductionQueryForm.aspx.cs" Inherits="Module_Production_Queries_ProductionQueryForm" %>

<%@ Register Src="../Controls/ProductionQueryForm.ascx" TagName="ProductionQueryForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:ProductionQueryForm ID="ProductionQueryForm1" runat="server" />
</asp:Content>
