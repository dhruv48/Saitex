<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Machine_Process_Master.aspx.cs" Inherits="Module_Machine_Pages_Machine_Process_Master" Title="Untitled Page" %>
<%@ Register src="../Controls/Machine_Process_Master.ascx" tagname="Machine_Process_Master" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">   
    <uc1:Machine_Process_Master ID="Machine_Process_Master1" runat="server" />   
</asp:Content>

