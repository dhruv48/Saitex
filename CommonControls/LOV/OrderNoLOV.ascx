<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderNoLOV.ascx.cs" Inherits="CommonControls_LOV_OrderNoLOV" %>
<asp:DropDownList ID="ddlOrderNo" runat="server" AppendDataBoundItems="True" 
    AutoPostBack="True" onselectedindexchanged="ddlOrderNo_SelectedIndexChanged" 
    Width="150px">
</asp:DropDownList>