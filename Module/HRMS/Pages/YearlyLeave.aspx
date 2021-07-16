<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="YearlyLeave.aspx.cs" Inherits="Module_HRMS_Pages_YearlyLeave" Title="YearlyLeave" %>
<%@ Register src="../Controls/YearlyLeave.ascx" tagname="YearlyLeave" tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
 <uc1:YearlyLeave ID="YearlyLeave" runat="server" />  
</asp:Content>

