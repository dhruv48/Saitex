<%@ Control Language="C#" AutoEventWireup="true" CodeFile="STArticle.ascx.cs" Inherits="CommonControls_LOV_STArticle" %>
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
        margin-left: 2px;
    }
    .c1
    {
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
</style>
<cc1:ComboBox ID="ddlSTArticle" runat="server" AutoPostBack="True" CssClass="SmallFont"
    DataTextField="YARN_CODE" DataValueField="YARN_DATA" EnableLoadOnDemand="true"
    Height="200px" MenuWidth="400px" OnLoadingItems="ddlSTArticle_LoadingItems" OnSelectedIndexChanged="ddlSTArticle_SelectedIndexChanged"
  Mode="TextBox"  TabIndex="1" Width="100px" EnableVirtualScrolling="true">
    <HeaderTemplate>
        <div class="header c1">
            YARN CODE</div>
        <div class="header c2">
            DESCRIPTION</div>
    </HeaderTemplate>
    <ItemTemplate>
        <div class="item c1">
            <asp:Literal ID="Container4" runat="server" Text='<%# Eval("YARN_CODE") %>' />
        </div>
        <div class="item c2">
            <asp:Literal ID="Container5" runat="server" Text='<%# Eval("YARN_DESC") %>' />
        </div>
    </ItemTemplate>
    <FooterTemplate>
        Displaying items
        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
        out of
        <%# Container.ItemsCount %>.
    </FooterTemplate>
</cc1:ComboBox>
