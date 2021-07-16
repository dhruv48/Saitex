<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Processing_Standard_Master.aspx.cs" Inherits="Module_Machine_Pages_Processing_Standard_Master" Title="Untitled Page" %>

<%@ Register src="../Controls/Processing_Standard_Master.ascx" tagname="Processing_Standard_Master" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Processing_Standard_Master ID="Processing_Standard_Master1" 
        runat="server" />
</asp:Content>

