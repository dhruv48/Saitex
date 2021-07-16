<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OC_Approval.aspx.cs" Inherits="Module_OrderDevelopment_Pages_OC_Approval" Title="Untitled Page" %>

<%@ Register src="../Controls/OC_APPROVAL.ascx" tagname="OC_APPROVAL" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:OC_APPROVAL ID="OC_APPROVAL1" runat="server" />
</asp:Content>

