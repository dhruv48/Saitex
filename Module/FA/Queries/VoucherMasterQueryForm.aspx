<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="VoucherMasterQueryForm.aspx.cs" Inherits="Module_FA_Queries_VoucherMasterQueryForm" Title="Untitled Page" %>

<%@ Register src="../Controls/VoucherMasterQueryForm.ascx" tagname="VoucherMasterQueryForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:VoucherMasterQueryForm ID="VoucherMasterQueryForm1" runat="server" />
</asp:Content>

