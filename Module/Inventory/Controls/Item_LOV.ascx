<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Item_LOV.ascx.cs" Inherits="Module_Inventory_Controls_Item_LOV" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
</style>
<cc1:ComboBox ID="cmb_Item_LOV" runat="server" DataTextField="ITEM_DESC" DataValueField="ITEM_CODE"
    OnLoadingItems="cmb_Item_LOV_LoadingItems1" OnSelectedIndexChanged="cmb_Item_LOV_SelectedIndexChanged"
    AutoPostBack="True" EnableLoadOnDemand="True">
    <HeaderTemplate>
        <div class="header c1">
            ITEM CODE</div>
        <div class="header c2">
            ITEM DESCRIPTION</div>
        <div class="header c3">
            TYPE</div>
    </HeaderTemplate>
    <ItemTemplate>
        <div class="item c1">
            <%# Eval("ITEM_CODE") %></div>
        <div class="item c2">
            <%# Eval("ITEM_DESC") %></div>
        <div class="item c3">
            <%# Eval("ITEM_TYPE") %></div>
    </ItemTemplate>
    <FooterTemplate>
        Displaying items
        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
        out of
        <%# Container.ItemsCount %>.
    </FooterTemplate>
</cc1:ComboBox>
