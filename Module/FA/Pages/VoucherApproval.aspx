<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="VoucherApproval.aspx.cs" Inherits="Module_FA_Pages_VoucherApproval" Title="Untitled Page" %>

<%@ Register src="../Controls/VoucherApproved.ascx" tagname="VoucherApproved" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:VoucherApproved ID="VoucherApproved1" runat="server" />
</asp:Content>

