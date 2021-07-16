<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Item_QC_MasterList.aspx.cs" Inherits="Module_Inventory_Pages_Item_QC_MasterList" %>
<%@ Register Src="~/Module/Inventory/Controls/Item_QC_MasterList.ascx" TagName="Item_QC_MasterList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<cc1:Item_QC_MasterList ID="Item_QC_MasterList1" runat="server" />
</asp:Content>

