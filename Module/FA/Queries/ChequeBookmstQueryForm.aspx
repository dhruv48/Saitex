<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ChequeBookmstQueryForm.aspx.cs" Inherits="Module_FA_Queries_ChequeBookmstQueryForm" Title="Untitled Page" %>

<%@ Register src="../Controls/ChequeBookMstQueryForm.ascx" tagname="ChequeBookMstQueryForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ChequeBookMstQueryForm ID="ChequeBookMstQueryForm1" runat="server" />
</asp:Content>


