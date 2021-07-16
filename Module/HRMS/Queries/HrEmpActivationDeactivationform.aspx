<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="HrEmpActivationDeactivationform.aspx.cs" Inherits="Module_HRMS_Queries_HrEmpActivationDeactivationform" Title="Untitled Page" %>

<%@ Register src="../Controls/HrEmpActivationDeactivationform.ascx" tagname="HrEmpActivationDeactivationform" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:HrEmpActivationDeactivationform ID="HrEmpActivationDeactivationform1" 
        runat="server" />
</asp:Content>

