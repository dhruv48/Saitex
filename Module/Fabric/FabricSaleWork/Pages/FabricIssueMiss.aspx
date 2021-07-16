<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricIssueMiss.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_FabricIssueMiss" %>

<%@ Register src="../Controls/FabricIssueMiss1.ascx" tagname="FabricIssueMiss1" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FabricIssueMiss1 ID="FabricIssueMisc1" runat="server" />
</asp:Content>

