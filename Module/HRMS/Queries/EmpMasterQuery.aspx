<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="EmpMasterQuery.aspx.cs" Inherits="Module_HRMS_Queries_EmpMasterQuery" %>

<%@ Register src="../Controls/EmpMasterQuery.ascx" tagname="EmpMasterQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:EmpMasterQuery ID="EmpMasterQuery1" runat="server" />
</asp:Content>

