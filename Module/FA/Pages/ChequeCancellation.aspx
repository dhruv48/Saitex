<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ChequeCancellation.aspx.cs" Inherits="Module_FA_Pages_ChequeCancellation" Title="Untitled Page" %>

<%@ Register src="../Controls/ChequeCancel.ascx" tagname="ChequeCancel" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ChequeCancel ID="ChequeCancel1" runat="server" />
</asp:Content>

