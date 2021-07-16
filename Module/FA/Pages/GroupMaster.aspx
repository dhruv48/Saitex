<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="GroupMaster.aspx.cs" Inherits="FA_Pages_GroupMaster" Title="Untitled Page" %>

<%@ Register Src="../Controls/GroupMaster.ascx" TagName="GroupMaster" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:GroupMaster ID="GroupMaster1" runat="server" />
</asp:Content>

