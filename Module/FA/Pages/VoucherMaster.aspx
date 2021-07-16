<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="VoucherMaster.aspx.cs" Inherits="Module_FA_Pages_VoucherMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/VoucherMaster.ascx" tagname="VoucherMaster" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:VoucherMaster ID="VoucherMaster1" runat="server" />
</asp:Content>

