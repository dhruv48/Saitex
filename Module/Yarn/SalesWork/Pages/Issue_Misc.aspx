<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Issue_Misc.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Issue_Misc" Title="Untitled Page" %>

<%@ Register src="../Controls/Issue.ascx" tagname="Issue" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Issue ID="Issue1" runat="server" />
</asp:Content>

