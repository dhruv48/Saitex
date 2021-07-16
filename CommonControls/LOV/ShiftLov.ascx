<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShiftLov.ascx.cs" Inherits="CommonControls_LOV_ShiftLov" %>
<asp:DropDownList ID="ddlShift" runat="server" 
    AppendDataBoundItems="True"  AutoPostBack="True"
    Width="150px" onselectedindexchanged="ddlShift_SelectedIndexChanged">
</asp:DropDownList>
