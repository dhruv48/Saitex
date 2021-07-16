<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FiberGateIn.aspx.cs" Inherits="Module_GateEntry_Pages_FiberGateIn" %>

<%@ Register src="~/Module/GateEntry/Controls/FiberGateEntry.ascx" tagname="FiberGateEntry" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">    
    <uc1:FiberGateEntry ID="FiberGateEntry1" runat="server" />   
</asp:Content>

   