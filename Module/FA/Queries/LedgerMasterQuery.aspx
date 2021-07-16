<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LedgerMasterQuery.aspx.cs" Inherits="Module_FA_Queries_LedgerMasterQuery" Title="Untitled Page" %>

<%@ Register src="../Controls/LedgerMaster.ascx" tagname="LedgerMaster" tagprefix="uc1" %>
<%@ Register src="../Controls/LedgerMasterQueryForm.ascx" tagname="LedgerMasterQueryForm" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:LedgerMasterQueryForm ID="LedgerMasterQueryForm1" runat="server" />
</asp:Content>

