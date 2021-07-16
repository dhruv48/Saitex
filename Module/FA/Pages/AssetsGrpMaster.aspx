<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="AssetsGrpMaster.aspx.cs" Inherits="Module_FA_Pages_AssetsGrpMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/AssetGrpMst.ascx" tagname="AssetGrpMst" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:AssetGrpMst ID="AssetGrpMst1" runat="server" />
</asp:Content>

