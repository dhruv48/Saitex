<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OnLineDrCrNoteGenerate.aspx.cs" Inherits="Module_FA_Pages_OnLineDrCrNoteGenerate" %>

<%@ Register src="../Controls/OnLineDrCrGenerate.ascx" tagname="OnLineDrCrGenerate" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:OnLineDrCrGenerate ID="OnLineDrCrGenerate1" runat="server" />
</asp:Content>

