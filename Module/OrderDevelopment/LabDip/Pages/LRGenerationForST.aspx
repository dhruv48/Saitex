<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LRGenerationForST.aspx.cs" Inherits="Module_OrderDevelopment_LabDip_Pages_LRGenerationForST" Title="Untitled Page" %>

<%@ Register src="../Controls/LRGenerationNew.ascx" tagname="LRGenerationNew" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:LRGenerationNew ID="LRGenerationNew1" runat="server" />
</asp:Content>

