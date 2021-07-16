<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/CommonMaster/admin.master"
    AutoEventWireup="true" CodeFile="AddChildMenu.aspx.cs" Inherits="Admin_AddChildMenu"
    Title="Add Child Menu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Controls/AddChildModule.ascx" tagname="AddChildModule" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">

    <uc1:AddChildModule ID="AddChildModule1" runat="server" />

</asp:Content>
