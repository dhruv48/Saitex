<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentLOV.ascx.cs" Inherits="CommonControls_LOV_DepartmentLOV" %>
<asp:DropDownList ID="ddlDepartment" runat="server" 
    AppendDataBoundItems="True"  AutoPostBack="True"
    Width="150px" onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
</asp:DropDownList>
