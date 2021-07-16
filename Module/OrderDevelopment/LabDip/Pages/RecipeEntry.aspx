<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="RecipeEntry.aspx.cs" Inherits="Module_OrderDevelopment_Pages_RecipeEntry" Title="Untitled Page" %>

<%@ Register src="../Controls/Recipe_Entry.ascx" tagname="Recipe_Entry" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Recipe_Entry ID="Recipe_Entry1" runat="server" />
</asp:Content>

