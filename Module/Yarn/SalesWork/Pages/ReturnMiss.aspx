<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ReturnMiss.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_ReturnMiss" Title="Untitled Page" %>

<%@ Register src="../Controls/ReturnMiss.ascx" tagname="ReturnMiss" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ReturnMiss ID="ReturnMiss1" runat="server" />
</asp:Content>

