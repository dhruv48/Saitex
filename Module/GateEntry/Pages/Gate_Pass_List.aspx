<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Gate_Pass_List.aspx.cs" Inherits="Module_GateEntry_Pages_Gate_Pass_List" %>
<%@ Register Src="~/Module/GateEntry/Controls/Gate_Pass_List.ascx" TagName="Gate_Pass_List" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Gate_Pass_List id="Gate_Pass_List" runat = "server"></uc1:Gate_Pass_List>
</asp:Content>

