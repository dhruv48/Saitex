<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Po_Unapproval.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Po_Unapproval" Title="PO Unapproval" %>

<%@ Register src="../Controls/Po_Unapproval.ascx" tagname="Po_Unpproval" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Po_Unpproval ID="Po_Unpproval1" runat="server" />
</asp:Content>

