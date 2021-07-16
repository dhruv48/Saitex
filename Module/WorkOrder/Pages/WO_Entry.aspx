
<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="WO_Entry.aspx.cs" Inherits="Module_WorkOrder_Pages_WO_Entry" Title="Untitled Page" %>
<%@ Register src="../Controls/Work_order_entry.ascx" tagname="Work_order_entry" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Work_order_entry ID="Work_order_entry1" runat="server" />
</asp:Content>

