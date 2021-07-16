<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="WIPStockQueryFormTwisted.aspx.cs" Inherits="Module_Production_Queries_WIPStockQueryFormTwisted" %>

<%@ Register Src="../Controls/WIPStockQueryFormTwisted.ascx" TagName="WIPStockQueryForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:WIPStockQueryForm ID="WIPStockQueryForm1" runat="server" />
</asp:Content>
