<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ChequeCancellationQuery.aspx.cs" Inherits="Module_FA_Queries_ChequeCancellationQuery" Title="Untitled Page" %>

<%@ Register src="../Controls/ChequeCancelQuery.ascx" tagname="ChequeCancelQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ChequeCancelQuery ID="ChequeCancelQuery1" runat="server" />
</asp:Content>

