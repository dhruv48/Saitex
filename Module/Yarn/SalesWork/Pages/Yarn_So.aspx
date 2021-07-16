<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Yarn_So.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Yarn_So" Title="Untitled Page" %>

<%@ Register src="../Controls/YARN_SO.ascx" tagname="YARN_SO" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
      <uc1:YARN_SO ID="YARN_SO1" runat="server" />
        </asp:Content>
