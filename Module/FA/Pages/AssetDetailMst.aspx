<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="AssetDetailMst.aspx.cs" Inherits="Module_FA_Pages_AssetDetailMst"
    Title="Untitled Page" %>

<%@ Register Src="../Controls/AssetDTL.ascx" TagName="AssetDTL" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:AssetDTL ID="AssetDTL1" runat="server" />
</asp:Content>
