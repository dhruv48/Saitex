<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="PO_Unapproval.aspx.cs" Inherits="Module_Inventory_Pages_PO_Unapproval" Title="Material PO Unapproval" %>

<%@ Register src="../Controls/PO_Unapproval.ascx" tagname="PO_Unapproval" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server" > 
    <uc1:PO_Unapproval ID="PO_Unapproval1" runat="server" />
</asp:Content>

