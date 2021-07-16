<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="RateComponent.aspx.cs" Inherits="Module_Inventory_Pages_RateComponent" Title="Untitled Page" %>

<%@ Register src="../Controls/RateComponent.ascx" tagname="RateComponent" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:RateComponent ID="RateComponent1" runat="server" />
</asp:Content>

