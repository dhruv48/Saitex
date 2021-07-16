<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CreditNote.aspx.cs" Inherits="Module_Inventory_Pages_CreditNote" %>

<%@ Register src="../Controls/CreditNote.ascx" tagname="DebitNote" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:DebitNote ID="DebitNote1" runat="server" />
</asp:Content>

