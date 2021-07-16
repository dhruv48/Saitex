<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Material Issue Against PI.aspx.cs" Inherits="Module_Inventory_Pages_Material_Issue_Against_PI" Title="Untitled Page" %>

<%@ Register src="../Controls/Metrial_Issue_Agin.ascx" tagname="Metrial_Issue_Agin" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Metrial_Issue_Agin ID="Metrial_Issue_Agin1" runat="server" />
</asp:Content>

