<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Designationlevel.aspx.cs" Inherits="Module_Admin_Pages_Designationlevel" Title="Untitled Page" %>

<%@ Register src="../Controls/Designationlevel.ascx" tagname="Designationlevel" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Designationlevel ID="Designationlevel1" runat="server" />
</asp:Content>

