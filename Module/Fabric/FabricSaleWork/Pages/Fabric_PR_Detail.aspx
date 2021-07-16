<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fabric_PR_Detail.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_Fabric_PR_Detail" %>

<%@ Register src="../Controls/Fabric_PR_Details.ascx" tagname="Fabric_PR_Details" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Fabric_PR_Details ID="Fabric_PR_Details1" runat="server" />
</asp:Content>

