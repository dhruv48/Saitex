<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="BankMstReport.aspx.cs" Inherits="Module_FA_Reports_BankMstReport" Title="Untitled Page" %>

<%@ Register src="../Controls/BankMstReport.ascx" tagname="BankMstReport" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:BankMstReport ID="BankMstReport1" runat="server" />
</asp:Content>

