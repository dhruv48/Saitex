<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Recipe_Query_Form.aspx.cs" Inherits="Module_OrderDevelopment_LabDip_Pages_Recipe_Query_Form" Title="Untitled Page" %>
<%@ Register src="~/Module/OrderDevelopment/LabDip/Controls/Recipe_Query_From1.ascx" tagname="RecipeQueryForm" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:RecipeQueryForm ID="RecipeQueryForm" runat="server" />
</asp:Content>

