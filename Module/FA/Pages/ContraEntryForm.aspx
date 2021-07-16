<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ContraEntryForm.aspx.cs" Inherits="Module_FA_Pages_ContraEntryForm" Title="Untitled Page" %>

<%@ Register src="../Controls/ContraEntry.ascx" tagname="ContraEntry" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    
    <uc1:ContraEntry ID="ContraEntry1" runat="server" />
    
</asp:Content>

