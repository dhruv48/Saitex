<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="BranchMaster.aspx.cs" Inherits="Module_Admin_Pages_BranchMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/BranchMaster.ascx" tagname="BranchMaster" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:BranchMaster ID="BranchMaster" runat="server" />
</asp:Content>

