<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="OnLineSaleGenerate.aspx.cs" Inherits="Module_FA_Pages_OnLineSaleGenerate"
    Title="Untitled Page" %>

<%@ Register src="../Controls/OnLineSaleGenerate.ascx" tagname="OnLineSaleGenerate" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:OnLineSaleGenerate ID="OnLineSaleGenerate1" runat="server" />
</asp:Content>
