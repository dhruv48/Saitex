<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="DrCrClearance.aspx.cs" Inherits="Module_Inventory_Pages_DrCrClearance" %>

<%@ Register src="../Controls/DebitClearance.ascx" tagname="DebitClearance" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:DebitClearance ID="DebitClearance1" runat="server" />
</asp:Content>

