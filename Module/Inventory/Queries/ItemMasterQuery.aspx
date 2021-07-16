<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="ItemMasterQuery.aspx.cs" Inherits="Module_Inventory_Queries_ItemMasterQuery"
    Title="Untitled Page" %>

<%@ Register Src="../Controls/ItemMasterQry.ascx" TagName="ItemMasterQry" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:ItemMasterQry ID="ItemMasterQuery1" runat="server" />
</asp:Content>
