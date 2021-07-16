<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="YARN_CAT.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_YARN_CAT" %>

<%@ Register src="../Controls/yarn_cat.ascx" tagname="yarn_cat" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:yarn_cat ID="yarn_cat1" runat="server" />
</asp:Content>

