<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="PendingAdvancedAdvice.aspx.cs" Inherits="Module_FA_Pages_PendingAdvancedAdvice" Title="Untitled Page" %>

<%@ Register src="../Controls/UnadjustedAdvice.ascx" tagname="UnadjustedAdvice" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:UnadjustedAdvice ID="UnadjustedAdvice1" runat="server" />
</asp:Content>

