<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LotWiseRecipeEntry.aspx.cs" Inherits="Module_OrderDevelopment_LabDip_Pages_LotWiseRecipeEntry" %>
<%@ Register src="../Controls/LotWiseRecipeEntry.ascx" tagname="LotWiseRecipeEntry" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:LotWiseRecipeEntry ID="LotWiseRecipeEntry" runat="server" />
</asp:Content>


