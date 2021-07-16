<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="AdvancedAdviceConfirmation.aspx.cs" Inherits="Module_FA_Pages_AdvancedAdviceConfirmation" Title="Untitled Page" %>

<%@ Register src="../Controls/AdvancedAdviceConfirm.ascx" tagname="AdvancedAdviceConfirm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:AdvancedAdviceConfirm ID="AdvancedAdviceConfirm1" runat="server" />
</asp:Content>

