<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master"  CodeFile="Debit_Note_Parameter.aspx.cs" Inherits="Module_Inventory_Reports_Debit_Note_Parameter" %>

<%@ Register src="~/Module/Inventory/Controls/Debit_Note_Parameter.ascx" tagname="POPermrpt" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:POPermrpt ID="POPermrpt1" runat="server" />
</asp:Content>
