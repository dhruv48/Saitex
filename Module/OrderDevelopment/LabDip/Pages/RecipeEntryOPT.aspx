<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="RecipeEntryOPT.aspx.cs" Inherits="Module_OrderDevelopment_LabDip_Pages_RecipeEntryOPT" Title="Untitled Page" %>

<%@ Register src="../Controls/RecipeEntry.ascx" tagname="RecipeEntry" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

    <uc1:RecipeEntry ID="RecipeEntry1" runat="server" />

</asp:Content>

