<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Dyeing_Pending.aspx.cs" Inherits="Module_Production_Pages_Dyeing_Pending" %>
<%@ Register Src="~/Module/Production/Controls/Dyeing_Pending.ascx" TagName="Dyeing_Pending" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Dyeing_Pending id="Dyeing_Pending" runat = "server"></uc1:Dyeing_Pending>
</asp:Content>

