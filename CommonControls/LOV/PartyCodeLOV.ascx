<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PartyCodeLOV.ascx.cs"
    Inherits="CommonControls_PartyCodeLOV" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
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
        width: 80px;
    }
    .c4
    {
        margin-left: 4px;
        width: 200px;
    }
</style>
<%--<asp:DropDownList ID="ddlPartyLOV" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
    OnSelectedIndexChanged="ddlPartyLOV_SelectedIndexChanged" Width="140px" CssClass="SmallFont TextBox UpperCase">
</asp:DropDownList>--%>
<cc2:ComboBox ID="ddlPartyLOV" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
    OnLoadingItems="ddlPartyLOV_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
    EmptyText="Select Party" OnSelectedIndexChanged="ddlPartyLOV_SelectedIndexChanged"
    EnableVirtualScrolling="true" Width="150px" MenuWidth="500px" Height="200px">
    <HeaderTemplate>
        <div class="header c1">
            Code</div>
        <div class="header c4">
            NAME</div>
    </HeaderTemplate>
    <ItemTemplate>
        <div class="item c1">
            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
        <div class="item c4">
            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
    </ItemTemplate>
    <FooterTemplate>
        Displaying items
        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
        out of
        <%# Container.ItemsCount %>.
    </FooterTemplate>
</cc2:ComboBox>
