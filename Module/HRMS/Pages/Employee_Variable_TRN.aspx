<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Employee_Variable_TRN.aspx.cs" Inherits="Module_HRMS_Pages_Employee_Variable_TRN" Title="Untitled Page" %>

<%@ Register src="../Controls/Employe_Variable_trn.ascx" tagname="Employe_Variable_trn" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Employe_Variable_trn ID="Employe_Variable_trn1" runat="server" />
</asp:Content>

