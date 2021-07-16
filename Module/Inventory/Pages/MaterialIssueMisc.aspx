<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MaterialIssueMisc.aspx.cs" Inherits="Module_Inventory_Pages_MaterialIssueMisc" Title="Untitled Page" %>

<%@ Register src="../Controls/IssueMiss.ascx" tagname="IssueMiss" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:IssueMiss ID="IssueMiss1" runat="server" />
</asp:Content>

