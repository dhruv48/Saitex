<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="EmpMSTQuery.aspx.cs" Inherits="Module_HRMS_Pages_EmpMSTQuery" Title="Employee Master Query" %>


<%@ Register src="../Controls/EMP_MST_QUERY.ascx" tagname="EMP_MST_QUERY" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
   
    <uc1:EMP_MST_QUERY ID="EMP_MST_QUERY1" runat="server" />
   
</asp:Content>


