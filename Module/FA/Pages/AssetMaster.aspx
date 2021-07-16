<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="AssetMaster.aspx.cs" Inherits="Module_FA_Pages_AssetMaster"
    Title="Untitled Page" %>

<%@ Register Src="../Controls/AssetMst.ascx" TagName="AssetMst" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:AssetMst ID="AssetMst1" runat="server" />
</asp:Content>
