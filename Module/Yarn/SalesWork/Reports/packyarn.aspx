<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="packyarn.aspx.cs" Inherits="Module_Yarn_SalesWork_Reports_packyarn" %>
<%@ Register src="../Controls/Yarn_Pack.ascx" tagname="Yarn_Pack" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Yarn_Pack ID="Yarn_Pack1" runat="server" />
</asp:Content>


