<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Material_Report_Challan_Wise.aspx.cs" Inherits="Module_Inventory_Pages_Material_Report_Challan_Wise" Title="Untitled Page" %>

<%@ Register src="../Controls/Material_Master_Rpt_Challan_Wise.ascx" tagname="Material_Master_Rpt_Challan_Wise" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Material_Master_Rpt_Challan_Wise ID="Material_Master_Rpt_Challan_Wise1" 
        runat="server" />
</asp:Content>

