<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="GateEntryApproval.aspx.cs" Inherits="Module_GateEntry_Pages_GateEntryApproval" %>

<%@ Register src="../Controls/GateEntryApproval.ascx" tagname="GATE_ENTRY_Approval" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:GATE_ENTRY_Approval ID="GATE_ENTRY_Approval1" runat="server" />
</asp:Content>

