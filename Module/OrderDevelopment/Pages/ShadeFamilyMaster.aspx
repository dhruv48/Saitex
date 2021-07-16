<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ShadeFamilyMaster.aspx.cs" Inherits="Module_OrderDevelopment_Pages_ShadeFamilyMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/ShadeFamilyMaster.ascx" tagname="ShadeFamilyMaster" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ShadeFamilyMaster ID="ShadeFamilyMaster1" runat="server" />
</asp:Content>

