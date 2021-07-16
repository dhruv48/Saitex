<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="GroupMasterQueryForm.aspx.cs" Inherits="Module_FA_Queries_GroupMasterQueryForm" Title="Untitled Page" %>

<%@ Register src="../Controls/GroupMasterQueryForm.ascx" tagname="GroupMasterQueryForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:GroupMasterQueryForm ID="GroupMasterQueryForm1" runat="server" />
</asp:Content>

  