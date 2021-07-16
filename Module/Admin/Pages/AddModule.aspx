<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/CommonMaster/admin.master"
    AutoEventWireup="true" CodeFile="AddModule.aspx.cs" Inherits="Admin_AddModule"
    Title="Add Module" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Controls/ModuleMaster.ascx" tagname="ModuleMaster" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">

   
    
    <uc1:ModuleMaster ID="ModuleMaster1" runat="server" />

   
    
</asp:Content>
