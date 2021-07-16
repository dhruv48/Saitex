<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fiber_MST.aspx.cs" Inherits="Module_Fiber_Pages_Fiber_MST" Title="Untitled Page" %>

<%@ Register src="../Controls/FIBER_MST.ascx" tagname="FIBER_MST" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FIBER_MST ID="FIBER_MST1" runat="server" />
</asp:Content>

