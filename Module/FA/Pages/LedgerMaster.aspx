<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LedgerMaster.aspx.cs" Inherits="FA_Pages_LedgerMaster" Title="Untitled Page" %>

<%@ Register Src="../Controls/LedgerMaster.ascx" TagName="LedgerMaster" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:LedgerMaster id="LedgerMaster1" runat="server">
    </uc1:LedgerMaster>
</asp:Content>

