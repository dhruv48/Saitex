<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Production.aspx.cs" Inherits="Module_Production_Pages_Production" Title="Untitled Page" %>

<%@ Register src="../Controls/Production.ascx" tagname="Production" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Production ID="Production1" runat="server" />
</asp:Content>

