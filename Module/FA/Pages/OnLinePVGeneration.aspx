<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OnLinePVGeneration.aspx.cs" Inherits="Module_FA_Pages_OnLinePVGeneration" Title="Untitled Page" %>

<%@ Register src="../Controls/OnLinePVGenerate.ascx" tagname="OnLinePVGenerate" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:OnLinePVGenerate ID="OnLinePVGenerate1" runat="server" />
</asp:Content>

