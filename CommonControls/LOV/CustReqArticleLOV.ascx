<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustReqArticleLOV.ascx.cs"
    Inherits="CommonControls_LOV_CustReqArticleLOV" %>
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
</style>
<%--<asp:DropDownList ID="ddlCustReq" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
    OnSelectedIndexChanged="ddlCustReq_SelectedIndexChanged" Width="20px">
</asp:DropDownList>--%>
<cc2:ComboBox ID="ddlCustReq" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
    OnLoadingItems="ddlCustReq_LoadingItems" DataTextField="CUST_REQ_NO" DataValueField="SEARCH_DATA"
    EmptyText="Select CR" OnSelectedIndexChanged="ddlCustReq_SelectedIndexChanged"
    EnableVirtualScrolling="true" Width="150px" MenuWidth="500px" Height="200px">
    <HeaderTemplate>
        <div class="header c1">
            CR No</div>
        <div class="header c1">
            Article</div>
        <div class="header c1">
            Shade Name</div>
    </HeaderTemplate>
    <ItemTemplate>
        <div class="item c1">
            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("CUST_REQ_NO") %>' /></div>
        <div class="item c1">
            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ARTICLE_CODE") %>' /></div>
        <div class="item c1">
            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("SHADE_NAME") %>' /></div>
    </ItemTemplate>
    <FooterTemplate>
        Displaying items
        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
        out of
        <%# Container.ItemsCount %>.
    </FooterTemplate>
</cc2:ComboBox>
