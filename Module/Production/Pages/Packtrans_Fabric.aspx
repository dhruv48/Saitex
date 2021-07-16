<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Packtrans_Fabric.aspx.cs" Inherits="Module_Production_Pages_Packtrans_Fabric" %>

<%@ Register src="../Controls/Pack_trans_fabric.ascx" tagname="Pack_trans_fabric" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Pack_trans_fabric ID="Pack_trans_fabric1" runat="server" />
</asp:Content>

