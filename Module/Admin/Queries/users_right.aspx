<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Users_Right.aspx.cs" Inherits="Module_Admin_Queries_Users_Right" %>

<%@ Register src="../Controls/Users_Right_Query.ascx" tagname="Users_Right_Query" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Users_Right_Query ID="Users_Right_Query1" runat="server" />
</asp:Content>

