<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="BankBook.aspx.cs" Inherits="Module_FA_Pages_BankBook" Title="Untitled Page" %>
<%@ Register src="../Controls/GetBankBook.ascx" tagname="GetBankBook" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:GetBankBook ID="GetBankBook" runat="server" />
</asp:Content>

