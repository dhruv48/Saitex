<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="WasteManagement.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_WasteManagement" %>

<%@ Register src="../Controls/Waste_Management.ascx" tagname="Waste_Management" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:Waste_Management ID="Waste_Management1" runat="server" />
</asp:Content>
