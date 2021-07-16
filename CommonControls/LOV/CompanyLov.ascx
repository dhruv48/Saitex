<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CompanyLov.ascx.cs" Inherits="CommonControls_LOV_CompanyLov" %>
<asp:DropDownList ID="ddlCompany" runat="server" 
    AppendDataBoundItems="True"  AutoPostBack="True"
    Width="150px" onselectedindexchanged="ddlCompany_SelectedIndexChanged">
</asp:DropDownList>
