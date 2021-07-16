<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemCodeLOV.ascx.cs" Inherits="CommonControls_ItemCodeLOV" %>
<asp:DropDownList ID="ddlItemcodeLov" runat="server" 
    AppendDataBoundItems="True" AutoPostBack="True" 
    onselectedindexchanged="ddlItemcodeLov_SelectedIndexChanged" Width="250px">
</asp:DropDownList>
