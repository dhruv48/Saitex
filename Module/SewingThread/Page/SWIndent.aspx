<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SWIndent.aspx.cs" Inherits="Module_Sewing_Thread_Page_SWIndent" Title="Untitled Page" %>

<%@ Register src="../Controls/SWIndent.ascx" tagname="SWIndent" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:SWIndent ID="SWIndent1" runat="server" />
</asp:Content>

