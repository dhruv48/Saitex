<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="DepartmentMaster.aspx.cs" Inherits="Module_Admin_Pages_DepartmentMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/DepartmentMaster.ascx" tagname="DepartmentMaster" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <p>
        <uc1:DepartmentMaster ID="DepartmentMaster1" runat="server" />
    </p>
</asp:Content>

