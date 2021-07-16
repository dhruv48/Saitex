<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FiberLotWiseRecipeEntry.aspx.cs" Inherits="Module_OrderDevelopment_LabDip_Pages_FiberLotWiseRecipeEntry" %>
<%@ Register src="../Controls/FIBER_LotWiseRecipeEntry.ascx" tagname="FIBER_LotWiseRecipeEntry" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
      <uc1:FIBER_LotWiseRecipeEntry ID="FIBER_LotWiseRecipeEntry" runat="server" />
</asp:Content>

