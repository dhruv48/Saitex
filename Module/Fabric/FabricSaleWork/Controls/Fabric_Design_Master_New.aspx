<%@ Page Title="" Language="C#"  MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fabric_Design_Master_New.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_Fabric_Design_Master_New" %>

<%@ Register src="../Controls/FabricDesignNew.ascx" tagname="FabricDesignNew" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FabricDesignNew ID="FabricDesignNew1" runat="server" />
</asp:Content>

