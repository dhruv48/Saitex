<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Dispatch_Query_Form.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Dispatch_Query_Form" %>
<%@ Register src="~/Module/Yarn/SalesWork/Queries/Dispatch_Query_Form.ascx" tagname="Dispatch_Query_Form" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Dispatch_Query_Form ID="Dispatch_Query_Form" runat="server" />
</asp:Content>

