<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Item_QC_Master.aspx.cs" Inherits="Module_Inventory_Pages_Item_QC_Master" %>
<%@ Register Src="~/Module/Inventory/Controls/ITEM_QC_Master.ascx" TagName="ITEM_QC_Master" TagPrefix="uc1"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <uc1:ITEM_QC_Master ID="ReceivingCredit_new1" runat="server" />
</asp:Content>

