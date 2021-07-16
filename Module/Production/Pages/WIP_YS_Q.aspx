<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="WIP_YS_Q.aspx.cs" Inherits="Module_Production_Pages_WIP_YS_Q" %>

<%@ Register src="../Queries/WIP_4_YS.ascx" tagname="WIP_4_YS" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:WIP_4_YS ID="WIP_4_YS1" runat="server" />
</asp:Content>

