<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BUDGET_LOV.ascx.cs" 
Inherits="Module_HRMS_Controls_BUDGET_LOV" %>

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
        width: 50px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
</style>


<cc1:ComboBox ID="cmb_BUDGET_LOV" runat="server" DataTextField="DEPT_CODE" DataValueField="DEPT_BUDGET_CODE"
    OnLoadingItems="cmb_BUDGET_LOV_LoadingItems1" 
    OnSelectedIndexChanged="cmb_BUDGET_LOV_SelectedIndexChanged" AutoPostBack="True" 
    EnableLoadOnDemand="True">
    <HeaderTemplate>       
        <div class="header c1">
           DEPT CODE</div>
        <div class="header c2">
            YEAR</div>            
       <div class="header c3">
            BUDGET_AMT</div>
    </HeaderTemplate>
    <ItemTemplate>
        
        <div class="item c1">
            <%# Eval("DEPT_CODE")%></div>
        <div class="item c2">
            <%# Eval("YEAR")%></div>
         <div class="item c3">
            <%# Eval("BUDGET_AMT")%></div>
    </ItemTemplate>
    <FooterTemplate>
        Displaying items
        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
        out of
        <%# Container.ItemsCount %>.
    </FooterTemplate>
</cc1:ComboBox>
