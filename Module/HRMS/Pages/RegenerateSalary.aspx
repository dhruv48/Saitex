<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"  CodeFile="RegenerateSalary.aspx.cs" Inherits="Module_HRMS_Pages_RegenerateSalary"
    Title="Re-Generate Employee Salary" %>
<%@ Register src="../Controls/Genrate_Salary.ascx" tagname="Genrate_Salary" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
       
    <uc1:Genrate_Salary ID="Genrate_Salary1" runat="server" />
       
</asp:Content>
