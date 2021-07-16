<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BranchLov.ascx.cs" Inherits="CommonControls_LOV_BranchLov" %>
<asp:DropDownList ID="ddlBranch" runat="server" 
    AppendDataBoundItems="True"  AutoPostBack="True"
    Width="150px" onselectedindexchanged="ddlBranch_SelectedIndexChanged">
</asp:DropDownList>
