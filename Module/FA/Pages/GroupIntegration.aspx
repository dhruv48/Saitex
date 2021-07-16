<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="GroupIntegration.aspx.cs" Inherits="Module_FA_Pages_GroupIntegration" Title="Untitled Page" %>

<%@ Register src="../Controls/GroupInt.ascx" tagname="GroupInt" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:GroupInt ID="GroupInt1" runat="server" />
</asp:Content>

