<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="Fabric_Indent.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_Fabric_Indent"
    Title="Fabric Indent" %>

<%@ Register Src="../Controls/Fabric_Indent.ascx" TagName="Fabric_Indent" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:Fabric_Indent ID="Fabric_Indent1" runat="server" />
</asp:Content>
