<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="UserMasterQuery.aspx.cs" Inherits="Module_Admin_Queries_UserMasterQuery" %>
<%@ Register Src="~/Module/Admin/Controls/UserMasterQuery1.ascx" tagname="UserMasterQuery1" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:UserMasterQuery1 ID="UserMasterQuery2" runat="server" /> 
</asp:Content>

