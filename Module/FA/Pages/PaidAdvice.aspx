<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="PaidAdvice.aspx.cs" Inherits="Module_FA_Pages_PaidAdvice" Title="Untitled Page" %>

<%@ Register src="../Controls/PaidAdvice.ascx" tagname="PaidAdvice" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:PaidAdvice ID="PaidAdvice1" runat="server" />
</asp:Content>

