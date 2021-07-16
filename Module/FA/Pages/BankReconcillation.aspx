<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="BankReconcillation.aspx.cs" Inherits="Module_FA_Pages_BankReconcillation" Title="Untitled Page" %>

<%@ Register src="../Controls/BankReconcillation.ascx" tagname="BankReconcillation" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:BankReconcillation ID="BankReconcillation1" runat="server" />
</asp:Content>


