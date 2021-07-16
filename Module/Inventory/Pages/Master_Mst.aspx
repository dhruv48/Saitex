<%@ Page Language="C#" MasterPageFile ="~/CommonMaster/admin.master" AutoEventWireup ="true" CodeFile="Master_Mst.aspx.cs" Inherits="Inventory_Master_Mst" Title="Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/Master_Mst.ascx" TagName="Master_Mst" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
 <%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>--%>
    <uc1:Master_Mst id="Master_Mst1" runat="server">
    </uc1:Master_Mst>
</asp:Content>

